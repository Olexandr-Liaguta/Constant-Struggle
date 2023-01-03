using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to store all data for a single tile
public class TileData
{
    public float[,] heightMap;
    public float[,] heatMap;
    public float[,] moistureMap;
    public TerrainType[,] chosenHeightTerrainTypes;
    public TerrainType[,] chosenHeatTerrainTypes;
    public TerrainType[,] chosenMoistureTerrainTypes;
    public Biome[,] chosenBiomes;
    public Mesh mesh;
    public Texture2D texture;

    public TileData(
        float[,] heightMap,
        float[,] heatMap,
        float[,] moistureMap,
        TerrainType[,] chosenHeightTerrainTypes,
        TerrainType[,] chosenHeatTerrainTypes,
        TerrainType[,] chosenMoistureTerrainTypes,
        Biome[,] chosenBiomes,
        Mesh mesh,
        Texture2D texture
     )
    {
        this.heightMap = heightMap;
        this.heatMap = heatMap;
        this.moistureMap = moistureMap;
        this.chosenHeightTerrainTypes = chosenHeightTerrainTypes;
        this.chosenHeatTerrainTypes = chosenHeatTerrainTypes;
        this.chosenMoistureTerrainTypes = chosenMoistureTerrainTypes;
        this.chosenBiomes = chosenBiomes;
        this.mesh = mesh;
        this.texture = texture;
    }

}
