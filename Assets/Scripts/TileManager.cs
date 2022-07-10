using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public static Tilemap baseTilemap;
    private static List<Tilemap> outlineTilemaps;

    private static Dictionary<Vector3Int, TileProperties> tilePropertiesDict;

    private static readonly Vector3Int[] directions = new Vector3Int[6]
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.forward,
            Vector3Int.back
        };

// Start is called before the first frame update
    void Start()
    {
        List<TileBase> tiles = new();
        List<Vector3Int> tileMapLocations = new();

        tilePropertiesDict = new();

        baseTilemap = GetComponent<Tilemap>();
        outlineTilemaps = new()
        {
            transform.Find("north").GetComponent<Tilemap>(),
            transform.Find("south").GetComponent<Tilemap>(),
            transform.Find("east").GetComponent<Tilemap>(),
            transform.Find("west").GetComponent<Tilemap>(),
        };

        foreach (var pos in baseTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new(pos.x, pos.y, pos.z);
            if (baseTilemap.HasTile(localPlace))
            {
                tileMapLocations.Add(localPlace);
                tiles.Add(baseTilemap.GetTile(localPlace));
                tilePropertiesDict.Add(localPlace, new(baseTilemap, localPlace));
            }
        }
        foreach(var tilemap in outlineTilemaps)
        {
            tilemap.SetTiles(tileMapLocations.ToArray(), tiles.ToArray());
        }
        FindNeighborsForAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void FindNeighborsForAllTiles()
    {
        foreach(var pos in tilePropertiesDict.Keys)
        {
            FindNeighbors(pos);
        }
    }

    static void FindNeighbors(Vector3Int pos)
    {
        TileProperties tileProperties = tilePropertiesDict[pos];
        tileProperties.Reset();
        tileProperties.ResetAdjacencyList();
        foreach (var direction in directions)
        {
            if(tilePropertiesDict.ContainsKey(pos + direction))
                tileProperties.adjacencyList[direction] = tilePropertiesDict[pos + direction];
        }
    }

    public static void ResetAll()
    {
        foreach (var tile in tilePropertiesDict.Values)
        {
            tile.Reset();
        }
    }

    public static bool CanPlaceUnit(Vector3Int tilePos, GameObject newUnit)
    {
        return tilePropertiesDict[tilePos].CanPlaceUnit(newUnit);
    }

    public static bool PlaceUnit(Vector3Int tilePos, GameObject newUnit)
    {
        if(tilePropertiesDict.ContainsKey(tilePos))
            return tilePropertiesDict[tilePos].PlaceUnit(newUnit);
        return false;
    }

    public static bool RemoveUnit(GameObject unit)
    {
        foreach (var pair in tilePropertiesDict)
        {
            if (pair.Value.unit == unit)
            {
                return pair.Value.RemoveUnit();
            }
        }
        return false;
    }

    public static bool RemoveUnit(Vector3Int pos)
    {
        if(tilePropertiesDict.ContainsKey(pos))
            return tilePropertiesDict[pos].RemoveUnit();
        return false;
    }

    public static bool MoveUnit(Vector3Int newPos, GameObject unit)
    {
        if (!tilePropertiesDict[newPos].CanPlaceUnit(unit)) { return false; }
        foreach(var pair in tilePropertiesDict)
        {
            if(pair.Value.unit == unit)
            {
                return tilePropertiesDict[newPos].PlaceUnit(tilePropertiesDict[pair.Key].RemoveUnit());
            }
        }
        return false;
    }

    public static bool MoveUnit(Vector3Int newPos, Vector3Int oldPos)
    {
        GameObject tempUnit = tilePropertiesDict[oldPos].unit;
        if (!tilePropertiesDict[newPos].CanPlaceUnit(tempUnit)) { return false; }
        return tilePropertiesDict[newPos].PlaceUnit(tilePropertiesDict[oldPos].RemoveUnit());
    }

    public static TileProperties GetTileAt(Vector3Int pos)
    {
        if(!tilePropertiesDict.ContainsKey(pos)) { Debug.Log("Null Tile"); }
        return tilePropertiesDict[pos];
    }

    public static void PlaceTile(TileBase tile, Vector3Int pos)
    {
        baseTilemap.SetTile(pos, tile);
        foreach (var tilemap in outlineTilemaps)
        {
            tilemap.SetTile(pos, tile);
        }
        if (!tilePropertiesDict.ContainsKey(pos))
            tilePropertiesDict.Add(pos, new TileProperties(baseTilemap, pos));
        FindNeighborsForAllTiles();
    }

    public static void RemoveTile(Vector3Int pos)
    {
        baseTilemap.SetTile(pos, null);
        foreach (var tilemap in outlineTilemaps)
        {
            tilemap.SetTile(pos, null);
        }
        tilePropertiesDict.Remove(pos);
        FindNeighborsForAllTiles();
    }
    
}
