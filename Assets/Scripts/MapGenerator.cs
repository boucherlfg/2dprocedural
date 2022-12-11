using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator map;
    public const int mapChunkSize = 241;
    public int width;
    public int height;
    public float scale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public DrawMode drawMode;
    public int seed;
    public Vector2 offset;

    public TerrainType[] regions;

    public bool autoUpdate = true;
    public void GenerateMap()
    {
        var noiseMap = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (value <= regions[i].height)
                    {
                        colorMap[y * width + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        var display = FindObjectOfType<MapDisplay>();
        Texture2D tex = null;
        if (drawMode == DrawMode.Noise)
        {
            tex = GOGenerator.TextureFromNoiseMap(noiseMap);
        }
        else if (drawMode == DrawMode.Color)
        {
            tex = GOGenerator.TextureFromColorMap(colorMap, width, height);
        }
        display.DrawTexture(tex);
    }

    void Start()
    {
        map = this;
       // GenerateMap();
    }
    void Update()
    {
        GenerateMap();
    }
    void OnValidate()
    {
        width = Mathf.Max(1, width);
        height = Mathf.Max(1, height);
        lacunarity = Mathf.Max(1, lacunarity);
        octaves = Mathf.Max(1, octaves);
    }
}