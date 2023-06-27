using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;
    [SerializeField] private Texture2D _roadTexture;

    [SerializeField] private float _roadWidth;
    [SerializeField] private float _roadLength;

    private float[,,] map;

    float scaleX;
    float scaleZ;
    private void Awake()
    {
        TerrainData terrainData = _terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        int alphamapResolution = terrainData.alphamapResolution;

        scaleX = terrainSize.x / alphamapResolution;
        scaleZ = terrainSize.z / alphamapResolution;

        map = new float[_terrain.terrainData.alphamapWidth, _terrain.terrainData.alphamapHeight, 2];
        for (int y = 0; y < _terrain.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < _terrain.terrainData.alphamapWidth; x++)
            {
                    map[x, y, 0] = 1;
                    map[x, y, 1] = 0;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, map);
    }

    public void DrawVertical(Vector3 position)
    {
        Vector2Int currentPosition = new Vector2Int((int)(position.z / scaleZ),(int)(position.x / scaleX));
        for (int y = currentPosition.y - (int)_roadLength / 2; y < currentPosition.y + (int)_roadLength / 2; y++)
        {
            for (int x = currentPosition.x - (int)_roadWidth / 2; x < currentPosition.x + (int)_roadWidth / 2; x++)
            {
                map[x, y, 0] = 0;
                map[x, y, 1] = 1;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, map);
    }

    public void DrawHorizontal(Vector3 position)
    {
        Vector2Int currentPosition = new Vector2Int((int)(position.z / scaleZ), (int)(position.x / scaleX));
        for (int y = currentPosition.y - (int)_roadWidth / 2; y < currentPosition.y + (int)_roadWidth / 2; y++)
        {
            for (int x = currentPosition.x - (int)_roadLength / 2; x < currentPosition.x + (int)_roadLength / 2; x++)
            {
                map[x, y, 0] = 0;
                map[x, y, 1] = 1;
            }
        }
        _terrain.terrainData.SetAlphamaps(0, 0, map);
    }
}