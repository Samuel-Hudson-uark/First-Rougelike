using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public bool turn = false;
    TileManager tileManager;

    List<TileProperties> selectableTiles;

    Stack<TileProperties> path = new Stack<TileProperties>();
    TileProperties currentTile;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();
    
    public bool moving = false;
    public float moveSpeed = 4;

    public UnitLogic logic;

    private void Start()
    {
        tileManager = GameObject.Find("Tilemap").GetComponent<TileManager>();
    }

    public void Init(Vector3Int pos)
    {
        this.currentTile = tileManager.findTile(pos);
        selectableTiles = new List<TileProperties>();
        logic = gameObject.GetComponent<UnitLogic>();

        TurnManager.AddUnit(this);
    }

    public TileManager GetTileManager() { return tileManager; }

    public void FindSelectableTiles()
    {
        tileManager.ResetAll();

        if (!logic.CanMove())
            return;

        Queue<TileProperties> process = new Queue<TileProperties>();
        process.Enqueue(currentTile);
        currentTile.visited = true;

        while(process.Count > 0)
        {
            TileProperties t = process.Dequeue();

            selectableTiles.Add(t);
            if(t != currentTile)
                t.selectable = true;

            if(t.distance < logic.currentMove)
            {
                foreach (var tile in t.adjacencyList.Values)
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

    public void MoveToTile(TileProperties tile)
    {
        path.Clear();
        moving = true;

        this.currentTile = tile;

        TileProperties next = tile;
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
            TileProperties t = path.Peek();
            Vector3 target = t.tilemap.CellToLocal(t.pos) + new Vector3(0, 0.75f, 0);
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

        foreach (var tile in selectableTiles)
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
