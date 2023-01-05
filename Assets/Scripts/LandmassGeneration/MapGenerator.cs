using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Threading;
using UnityEditor.PackageManager.Requests;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh }
    public DrawMode drawMode = DrawMode.NoiseMap;

    public const int mapChunkSize = 241;
    [Range(0, 6)]
    public int editorPreviewLOD;

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

    Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new();
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new();

    public void DrawMapInEditor()
    {
        MapData mapData = GenerateMapData(Vector2.zero);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(
                TextureGenerator.TextureFromColourMap(
                    colourMap: mapData.colourMap,
                    width: mapChunkSize,
                    height: mapChunkSize
                )
            );
        }
        else if (drawMode == DrawMode.Mesh)
        {
            Texture2D texture = TextureGenerator.TextureFromColourMap(
                                    colourMap: mapData.colourMap,
                                    width: mapChunkSize,
                                    height: mapChunkSize
                                );

            MeshData meshData = MeshGenerator.GenerateTerrainMesh(
                                    heightMap: mapData.heightMap,
                                    heightMultiplier: meshHeightMultiplier,
                                    heightCurve: meshHeightCurve,
                                    levelOfDetail: editorPreviewLOD
                                );

            display.DrawMesh(meshData: meshData, texture: texture);
        }
    }

    public void RequestMapData(Vector2 centre, Action<MapData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MapDataThread(centre, callback);
        };

        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 centre, Action<MapData> callback)
    {
        MapData mapData = GenerateMapData(centre);
        lock (mapDataThreadInfoQueue)
        {
            mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback: callback, parameter: mapData));
        }
    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(mapData, lod, callback);
        };

        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(
                heightCurve: meshHeightCurve,
                heightMultiplier: meshHeightMultiplier,
                heightMap: mapData.heightMap,
                levelOfDetail: lod
            );

        lock (meshDataThreadInfoQueue)
        {
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }

    private void Update()
    {
        if (mapDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < mapDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }

        if (meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    MapData GenerateMapData(Vector2 centre)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(
            mapWidth: mapChunkSize,
            mapHeight: mapChunkSize,
            octaves: octaves,
            scale: noiseScale,
            persistance: persistance,
            lacunarity: lacunarity,
            seed: seed,
            offset: centre + offset
            );

        Color[] colourMap = GenerateColourMap(noiseMap);

        return new MapData(heightMap: noiseMap, colourMap: colourMap);

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

    struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
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

public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    public MapData(float[,] heightMap, Color[] colourMap)
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}
