using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random rng = new System.Random(seed);
        Vector2[] offsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rng.Next(-100000, 100000) + offset.x;
            float offsetY = rng.Next(-100000, 100000) + offset.y;
            offsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoise = float.MinValue;
        float minNoise = float.MaxValue;
        float centerX = width / 2f;
        float centerY = height / 2f;

        float[,] noiseMap = new float[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - centerX) / scale * frequency + offsets[i].x;
                    float sampleY = (y - centerY) / scale * frequency + offsets[i].y;

                    float perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlin * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                maxNoise = Mathf.Max(noiseHeight, maxNoise);
                minNoise = Mathf.Min(noiseHeight, minNoise);
                noiseMap[x, y] = noiseHeight;
            }
            
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoise, maxNoise, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}