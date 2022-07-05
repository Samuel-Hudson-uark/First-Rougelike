using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iso_grid : MonoBehaviour
{
    public static int object_size = 1;
    [SerializeField] private Vector2Int grid_size;
    [SerializeField] private GameObject grid_tile;

    private GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[grid_size.x*grid_size.y];
        GenerateGrid();
    }

    // Update is called once per frame
    private void GenerateGrid()
    {
        for (int y = 0; y < grid_size.y; y++)
        {
            for (int x = 0; x < grid_size.x; x++)
            {
                GameObject tile = Instantiate(grid_tile, this.transform);
                tile.transform.localPosition = new Vector3((x-y)*0.5f, -(x+y)*0.25f);
                tile.name = $"Tile {x}, {y}";
                tile.GetComponent<SpriteRenderer>().sortingOrder = (y * grid_size.y + x)- (grid_size.x * grid_size.y);
                tiles[y*grid_size.y+x] = tile;
            }
        }
    }

    public GameObject FindTile(Vector2Int vector)
    {
        return FindTile(vector.x, vector.y);
    }

    public GameObject FindTile(int x, int y)
    {
        if(x < 0 || y < 0 || x >= grid_size.x || y >= grid_size.y)
        {
            return null;
        }
        return tiles[y*grid_size.y+x];
    }
}
