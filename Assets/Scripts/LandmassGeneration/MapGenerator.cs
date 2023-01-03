using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh }
    public DrawMode drawMode = DrawMode.NoiseMap;

    public const int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;

    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public TerrainType[] regions;

    public float meshHeightMultiplier = 1;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(
            mapWidth: mapChunkSize,
            mapHeight: mapChunkSize,
            octaves: octaves,
            scale: noiseScale,
            persistance: persistance,
            lacunarity: lacunarity,
            seed: seed,
            offset: offset
            );

        Color[] colourMap = GenerateColourMap(noiseMap);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(
                TextureGenerator.TextureFromColourMap(
                    colourMap: colourMap,
                    width: mapChunkSize,
                    height: mapChunkSize
                )
            );
        }
        else if (drawMode == DrawMode.Mesh)
        {
            Texture2D texture = TextureGenerator.TextureFromColourMap(
                                    colourMap: colourMap,
                                    width: mapChunkSize,
                                    height: mapChunkSize
                                );

            MeshData meshData = MeshGenerator.GenerateTerrainMesh(
                                    heightMap: noiseMap,
                                    heightMultiplier: meshHeightMultiplier,
                                    heightCurve: meshHeightCurve,
                                    levelOfDetail: levelOfDetail
                                );

            display.DrawMesh(meshData: meshData, texture: texture);
        }

    }

    private Color[] GenerateColourMap(float[,] noiseMap)
    {
        TerrainType[] filteredRegions = GetFilteredRegions();

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < filteredRegions.Length; i++)
                {
                    if (currentHeight <= filteredRegions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = filteredRegions[i].colour;
                        break;
                    }
                }
            }
        }

        return colourMap;
    }

    private TerrainType[] GetFilteredRegions()
    {
        TerrainType[] filteredTerrains = new TerrainType[regions.Length];

        float[] terrainsHeights = new float[regions.Length];
        for (int i = 0; i < regions.Length; i++)
        {
            terrainsHeights[i] = regions[i].height;
        }

        for (int i = 0; i < regions.Length; i++)
        {
            float minHeight = terrainsHeights.Min();
            TerrainType terrainType = regions.SingleOrDefault(region => region.height == minHeight);
            filteredTerrains[i] = terrainType;
            terrainsHeights = terrainsHeights.Where(height => height != minHeight).ToArray();
        }

        return filteredTerrains;
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}
