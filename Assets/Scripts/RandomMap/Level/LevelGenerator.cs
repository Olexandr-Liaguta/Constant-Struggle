using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private int levelWidthInTiles, levelDepthInTiles;

    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private TreeGeneration treeGeneration;

    [SerializeField]
    private RiverGeneration riverGeneration;

    private LevelGeneratorManager levelGeneratorManager;

    void Start()
    {
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;

        this.levelGeneratorManager = LevelGeneratorManager.instance;

        float maxDistanceZ = tileSize.z * levelWidthInTiles;
        levelGeneratorManager.maxDistanceZ = maxDistanceZ;
        levelGeneratorManager.centerVertexZ = maxDistanceZ / 2;

        GenerateMap();
    }
    void GenerateMap()
    {
        // get the tile dimensions from the tile Prefab
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;
        // calculate the number of vertices of the tile in each axis using its mesh
        Vector3[] tileMeshVertices = tilePrefab.GetComponent<MeshFilter>().sharedMesh.vertices;
        int tileDepthInVertices = (int)Mathf.Sqrt(tileMeshVertices.Length);
        int tileWidthInVertices = tileDepthInVertices;
        float distanceBetweenVertices = (float)tileDepth / (float)tileDepthInVertices;
        // build an empty LevelData object, to be filled with the tiles to be generated
        LevelData levelData = new LevelData(tileDepthInVertices, tileWidthInVertices, this.levelDepthInTiles, this.levelWidthInTiles);
        // for each Tile, instantiate a Tile in the correct position
        for (int xTileIndex = 0; xTileIndex < levelWidthInTiles; xTileIndex++)
        {
            for (int zTileIndex = 0; zTileIndex < levelDepthInTiles; zTileIndex++)
            {
                // calculate the tile position based on the X and Z indices
                Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + xTileIndex * tileWidth,
                  this.gameObject.transform.position.y,
                  this.gameObject.transform.position.z + zTileIndex * tileDepth);
                // instantiate a new Tile
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as GameObject;
                // generate the Tile texture and save it in the levelData
                TileData tileData = tile.GetComponent<TileGenerator>().GenerateTile(levelGeneratorManager.centerVertexZ, levelGeneratorManager.maxDistanceZ);
                levelData.AddTileData(tileData, zTileIndex, xTileIndex);
            }
        }
        // generate trees for the level
        treeGeneration.GenerateTrees(this.levelDepthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, distanceBetweenVertices, levelData);
        // generate rivers for the level
        riverGeneration.GenerateRivers(this.levelDepthInTiles * tileDepthInVertices, this.levelWidthInTiles * tileWidthInVertices, levelData);
    }

}
