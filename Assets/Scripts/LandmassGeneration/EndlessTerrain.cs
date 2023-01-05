using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    const float viewerMoveThresholdForChunkUpdate = 25f;
    const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

    public LODInfo[] detailLevels;
    public static float maxViewOst;

    public Transform viewer;
    public Material mapMaterial;

    public static Vector2 viewerPosition;
    Vector2 viewerPositionOld;
    static MapGenerator mapGenerator;
    int chunkSize;
    int chungVisibleInViewOst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new();
    List<TerrainChunk> terrainChunksVisibleLastUpdate = new();

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();

        maxViewOst = detailLevels[^1].visibleDstThreshold;
        chunkSize = MapGenerator.mapChunkSize - 1;
        chungVisibleInViewOst = Mathf.RoundToInt(maxViewOst / chunkSize);

        UpdateVisibleChunks();
    }

    private void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

        if ((viewerPosition - viewerPositionOld).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
        {
            viewerPositionOld = viewerPosition;
            UpdateVisibleChunks();
        }
    }

    void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }
        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chungVisibleInViewOst; yOffset < chungVisibleInViewOst; yOffset++)
        {
            for (int xOffset = -chungVisibleInViewOst; xOffset < chungVisibleInViewOst; xOffset++)
            {
                Vector2 viewedChunkCoord = new(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    TerrainChunk terrainChunk = terrainChunkDictionary[viewedChunkCoord];
                    terrainChunk.UpdateChunk();
                    if (terrainChunk.IsVisible())
                    {
                        terrainChunksVisibleLastUpdate.Add(terrainChunk);
                    }
                }
                else
                {
                    terrainChunkDictionary.Add(
                        viewedChunkCoord,
                        new TerrainChunk(
                            coord: viewedChunkCoord,
                            size: chunkSize,
                            parent: transform,
                            material: mapMaterial,
                            detailLevels: detailLevels
                            )
                    );
                }
            }
        }
    }

    public class TerrainChunk
    {

        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MapData mapData;
        bool mapDataReceived;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;
        int previousLODIndex = -1;

        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material, LODInfo[] detailLevels)
        {
            this.detailLevels = detailLevels;

            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;

            meshObject.transform.position = positionV3;
            meshObject.transform.parent = parent;
            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for (int i = 0; i < detailLevels.Length; i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateChunk);
            }

            mapGenerator.RequestMapData(position, OnMapDataReceived);
        }

        void OnMapDataReceived(MapData mapData)
        {
            this.mapData = mapData;
            this.mapDataReceived = true;

            Texture2D texture = TextureGenerator.TextureFromColourMap(
                colourMap: mapData.colourMap,
                width: MapGenerator.mapChunkSize,
                height: MapGenerator.mapChunkSize
                );
            meshRenderer.material.mainTexture = texture;


            UpdateChunk();
        }


        public void UpdateChunk()
        {
            if (!mapDataReceived) return;

            float viewerOstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerOstFromNearestEdge <= maxViewOst;

            if (visible)
            {
                int lodIndex = 0;

                for (int i = 0; i < detailLevels.Length - 1; i++)
                {
                    if (viewerOstFromNearestEdge > detailLevels[i].visibleDstThreshold)
                    {
                        lodIndex = i + 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (lodIndex != previousLODIndex)
                {
                    LODMesh lodMesh = lodMeshes[lodIndex];
                    if (lodMesh.hasMesh)
                    {
                        previousLODIndex = lodIndex;
                        meshFilter.mesh = lodMesh.mesh;
                    }
                    else if (!lodMesh.hasRequestedMesh)
                    {
                        lodMesh.RequestMesh(mapData);
                    }
                }
            }

            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh, hasMesh;
        int lod;
        System.Action updateCallback;

        public LODMesh(int lod, System.Action updateCallback)
        {
            this.lod = lod;
            this.updateCallback = updateCallback;
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            hasMesh = true;

            updateCallback();
        }

        public void RequestMesh(MapData mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, OnMeshDataReceived);
        }
    }

    [System.Serializable]
    public struct LODInfo
    {
        public int lod;
        public float visibleDstThreshold;
    }
}


