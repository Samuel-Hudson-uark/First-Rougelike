using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private int tileSize;
    [SerializeField] private Camera cam;

    void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var SpawnedTile = Instantiate(tilePrefab, new Vector3(x * tileSize, y * tileSize), Quaternion.identity);
                SpawnedTile.name = $"Tile {x}, {y}";
            }
        }
        cam.transform.position = new Vector3(tileSize*(width/2), tileSize*(height/2), -10);
    }
}
