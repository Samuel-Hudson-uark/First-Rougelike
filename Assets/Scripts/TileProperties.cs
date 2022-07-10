using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileProperties
{
    //TODO: Capability to store more than one unit (Fliers, small units, etc.)
    public GameObject unit;
    public Tilemap tilemap;
    public Vector3Int pos;

    private bool current;
    public bool Current
    {
        get => current;
        set
        {
            current = value;
            UpdateColor();
        }
    }
    private bool selectable;
    public bool Selectable
    {
        get => selectable;
        set
        {
            selectable = value;
            UpdateColor();
        }
    }
    private bool attackable;
    public bool Attackable
    {
        get => attackable;
        set
        {
            attackable = value;
            UpdateColor();
        }
    }

    public Dictionary<Vector3Int, TileProperties> adjacencyList;

    public bool visited;
    public TileProperties parent;
    public int distance;

    public TileProperties(Tilemap tilemap, Vector3Int pos)
    {
        Reset();
        ResetAdjacencyList();
        unit = null;
        this.tilemap = tilemap;
        this.pos = pos;
    }

    void UpdateColor()
    {
        Color color = Color.white;
        if (current)
        {
            color = Color.gray;
        }
        else if (attackable)
        {
            color = Color.red;
        }
        else if (selectable)
        {
            color = Color.green;
        }
        if(tilemap != null)
        {
            tilemap.SetTileFlags(pos, TileFlags.None);
            tilemap.SetColor(pos, color);
        }
    }

    public void ResetAdjacencyList()
    {
        adjacencyList = new Dictionary<Vector3Int, TileProperties>
        {
            { Vector3Int.up, null },
            { Vector3Int.down, null },
            { Vector3Int.right, null },
            { Vector3Int.left, null },
            { Vector3Int.forward, null },
            { Vector3Int.back, null }
        };
    }

    public void Reset()
    {

        current = false;
        attackable = false;
        selectable = false;
        UpdateColor();

        visited = false;
        parent = null;
        distance = 0;
    }

    public bool PlaceUnit(GameObject newUnit)
    {
        if (!CanPlaceUnit(unit)) { return false; }
        this.unit = newUnit;
        return true;
    }

    public GameObject RemoveUnit()
    {
        GameObject tempUnit = this.unit;
        unit = null;
        return tempUnit;
    }

    public bool CanPlaceUnit(GameObject unit)
    {
        //Tile logic for valid unit placement (land, sea, mountain, etc.) here.
        return this.unit == null;
    }

    public bool HasUnit(GameObject unit)
    {
        return this.unit == unit;
    }

    public Vector3 WorldPosForPlacement()
    {
        //adjust here for half-size blocks
        return tilemap.CellToWorld(pos) + new Vector3(0, 1f, 0);
    }
}
