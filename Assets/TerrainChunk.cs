using UnityEngine;

public class TerrainChunk
{
    GameObject prefab;
    Vector2 pos;
    Bounds bounds;
    public TerrainChunk(Vector2 pos, int size)
    {
        this.pos = pos * size;
        bounds = new Bounds(this.pos, Vector2.one * size);
        prefab = GameObject.CreatePrimitive(PrimitiveType.Plane);
        prefab.transform.position = this.pos;
        prefab.transform.localPosition = Vector3.one * size / 10f;
        SetVisible(false);
    }

    public void UpdateTerrainChunk(Vector2 viewerPosition, float maxViewDistance)
    {
        float distanceFromEdge = bounds.SqrDistance(viewerPosition);
        bool visible = distanceFromEdge <= maxViewDistance;
        SetVisible(visible);
    }
    public void SetVisible(bool visible)
    {
        prefab.SetActive(visible);
    }
}