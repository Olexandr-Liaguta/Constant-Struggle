using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to store all the merged tiles data
public class LevelData
{
    private int tileDepthInVertices, tileWidthInVertices;
    public TileData[,] tilesData;
    public LevelData(int tileDepthInVertices, int tileWidthInVertices, int levelDepthInTiles, int levelWidthInTiles)
    {
        // build the tilesData matrix based on the level depth and width
        tilesData = new TileData[tileDepthInVertices * levelDepthInTiles, tileWidthInVertices * levelWidthInTiles];
        this.tileDepthInVertices = tileDepthInVertices;
        this.tileWidthInVertices = tileWidthInVertices;
    }
    public void AddTileData(TileData tileData, int tileZIndex, int tileXIndex)
    {
        // save the TileData in the corresponding coordinate
        tilesData[tileZIndex, tileXIndex] = tileData;
    }

    public TileCoordinate ConvertToTileCoordinate(int zIndex, int xIndex)
    {
        // the tile index is calculated by dividing the index by the number of tiles in that axis
        int tileZIndex = (int)Mathf.Floor((float)zIndex / (float)this.tileDepthInVertices);
        int tileXIndex = (int)Mathf.Floor((float)xIndex / (float)this.tileWidthInVertices);
        // the coordinate index is calculated by getting the remainder of the division above
        // we also need to translate the origin to the bottom left corner
        int coordinateZIndex = this.tileDepthInVertices - (zIndex % this.tileDepthInVertices) - 1;
        int coordinateXIndex = this.tileWidthInVertices - (xIndex % this.tileDepthInVertices) - 1;
        TileCoordinate tileCoordinate = new TileCoordinate(tileZIndex, tileXIndex, coordinateZIndex, coordinateXIndex);
        return tileCoordinate;
    }
}