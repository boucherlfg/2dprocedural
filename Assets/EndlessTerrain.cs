using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    public const float maxViewDistance = 300;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunkCount;

    Dictionary<Vector2, TerrainChunk> terrainChunks = new Dictionary<Vector2, TerrainChunk>();
    // Start is called before the first frame update
    void Start()
    {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunkCount = Mathf.RoundToInt(maxViewDistance / chunkSize);
    }

    void UpdateVisibleChunks()
    {
        int chunkX = Mathf.RoundToInt(viewer.position.x / chunkSize);
        int chunkY = Mathf.RoundToInt(viewer.position.y / chunkSize);

        for (int yOff = -chunkCount; yOff <= chunkCount; yOff++)
        {
            for (int xOff = -chunkCount; xOff <= chunkCount; xOff++)
            {
                var chunkPos = new Vector2(chunkX + xOff, chunkY + yOff);

                if (terrainChunks.ContainsKey(chunkPos))
                {
                    terrainChunks[chunkPos].UpdateTerrainChunk(viewerPosition, maxViewDistance);
                }
                else
                {
                    terrainChunks.Add(chunkPos, new TerrainChunk(chunkPos, chunkSize));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.y);
        UpdateVisibleChunks();
    }
}
