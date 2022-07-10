using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInitializer : MonoBehaviour
{
    private Tilemap baseTilemap, northTilemap, southTilemap, eastTilemap, westTilemap;

    private List<TileBase> tiles;
    private List<Vector3Int> tileMapLocations;

    // Use this for initialization
    void Start()
    {
        baseTilemap = GetComponent<Tilemap>();
        northTilemap = transform.Find("north").GetComponent<Tilemap>();
        southTilemap = transform.Find("south").GetComponent<Tilemap>();
        eastTilemap = transform.Find("east").GetComponent<Tilemap>();
        westTilemap = transform.Find("west").GetComponent<Tilemap>();

        tiles = new List<TileBase>();
        tileMapLocations = new List<Vector3Int>();

        foreach (var pos in baseTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (baseTilemap.HasTile(localPlace))
            {
                tileMapLocations.Add(localPlace);
                tiles.Add(baseTilemap.GetTile(localPlace));
            }
        }
        
        northTilemap.SetTiles(tileMapLocations.ToArray(), tiles.ToArray());
        southTilemap.SetTiles(tileMapLocations.ToArray(), tiles.ToArray());
        eastTilemap.SetTiles(tileMapLocations.ToArray(), tiles.ToArray());
        westTilemap.SetTiles(tileMapLocations.ToArray(), tiles.ToArray());
    }
}
