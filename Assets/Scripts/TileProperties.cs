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
    /*
     * {
        get { return current; }
        set 
        { 
            current = value; 
            UpdateColor();
        }
    }
     */
    public bool current;
    public bool selectable;
    public bool attackable;

    public Dictionary<Vector3Int, TileProperties> adjacencyList;

    public bool visited;
    public TileProperties parent;
    public int distance;

    public TileProperties(Tilemap tilemap)
    {
        Reset();
        unit = null;
        this.tilemap = tilemap;
    }

    void UpdateColor()
    {
        Color color = Color.white;
        if (current)
        {
            color = Color.magenta;
        }
        else if (attackable)
        {
            color = Color.red;
        }
        else if (selectable)
        {
            color = Color.green;
        }
        tilemap.SetColor(pos, color);
    }

    public void Reset()
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

        current = false;
        attackable = false;
        selectable = false;

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
        return unit == null;
    }

    public bool HasUnit(GameObject unit)
    {
        return this.unit == unit;
    }
}
