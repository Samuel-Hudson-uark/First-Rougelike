using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool turn = false;

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();
    
    public bool moving = false;
    public float moveSpeed = 4;

    public UnitLogic logic;

    public void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        logic = gameObject.GetComponent<UnitLogic>();

        TurnManager.AddUnit(this);
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

        if (!logic.CanMove())
            return;

        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;

        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            if(t != currentTile)
                t.selectable = true;

            if(t.distance < logic.currentMove)
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

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        moving = true;

        transform.SetParent(tile.gameObject.transform);

        Tile next = tile;
        while(next != null)
        {
            path.Push(next);
            next = next.parent;
        }

        logic.currentMove -= path.Count;
        RemoveSelectableTiles();
    }

    public void Move()
    {
        if(path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;
            if(Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetVelocity();

                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            moving = false;
        }
    }
    
    protected void RemoveSelectableTiles()
    {
        if(currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }
        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetVelocity()
    {
        velocity = heading * moveSpeed;
    }

    public void BeginTurn()
    {
        logic.RefreshAttack();
        logic.RefreshMove();
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }
}
