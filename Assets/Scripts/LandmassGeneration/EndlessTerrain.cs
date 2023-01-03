using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{

    public const float maxViewOst = 450;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chungVisibleInViewOst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new();
    List<TerrainChunk> terrainChunksVisibleLastUpdate = new();

    void Start()
    {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chungVisibleInViewOst = Mathf.RoundToInt(maxViewOst / chunkSize);
    }

    private void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
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
                        new TerrainChunk(viewedChunkCoord, chunkSize, transform)
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

        public TerrainChunk(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateChunk()
        {
            float viewerOstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerOstFromNearestEdge <= maxViewOst;
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
}


