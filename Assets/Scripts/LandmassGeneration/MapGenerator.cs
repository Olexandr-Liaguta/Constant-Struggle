using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh }
    public DrawMode drawMode = DrawMode.NoiseMap;

    public int mapWidth, mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public TerrainType[] regions;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(
            mapWidth: mapWidth,
            mapHeight: mapHeight,
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
                    width: mapWidth,
                    height: mapHeight
                )
            );
        }
        else if (drawMode == DrawMode.Mesh)
        {
            Texture2D texture = TextureGenerator.TextureFromColourMap(
                                    colourMap: colourMap,
                                    width: mapWidth,
                                    height: mapHeight
                                );

            display.DrawMesh(
                meshData: MeshGenerator.GenerateTerrainMesh(noiseMap),
                texture: texture
            );
        }

    }

    private Color[] GenerateColourMap(float[,] noiseMap)
    {
        TerrainType[] filteredRegions = GetFilteredRegions();

        Color[] colourMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < filteredRegions.Length; i++)
                {
                    if (currentHeight <= filteredRegions[i].height)
                    {
                        colourMap[y * mapWidth + x] = filteredRegions[i].colour;
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
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }

        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

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
