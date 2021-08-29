using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    protected int move = 5;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void GetCurrentTile()
    {
        currentTile = getTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile getTargetTile(GameObject target)
    {
        return target.transform.parent.gameObject.GetComponent<Tile>();
    }

    public void ComputeAdjacencyLists()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors();
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;

        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if(t.distance < move)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }
}
