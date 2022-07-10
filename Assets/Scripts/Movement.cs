using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private Animator animator;

    public bool turn = false;

    List<TileProperties> selectableTiles;

    Stack<TileProperties> path = new();
    TileProperties currentTile;

    Vector3 velocity = new();
    Vector3 heading = new();
    
    public bool moving = false;
    public float moveSpeed = 4;

    public UnitLogic logic;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("moving", moving);
        animator.SetBool("attacking", logic.attacking);
    }

    public void Init(Vector3Int pos)
    {
        currentTile = TileManager.GetTileAt(pos);
        selectableTiles = new();
        logic = gameObject.GetComponent<UnitLogic>();
        logic.Init();

        TurnManager.AddUnit(this);
    }

    public void FindSelectableTiles()
    {
        TileManager.ResetAll();

        if (!logic.CanMove())
            return;

        Queue<TileProperties> process = new();
        process.Enqueue(currentTile);
        currentTile.visited = true;
        currentTile.Current = true;

        while(process.Count > 0)
        {
            TileProperties t = process.Dequeue();

            selectableTiles.Add(t);
            if(t != currentTile && t.CanPlaceUnit(gameObject))
                t.Selectable = true;

            if(t.distance < logic.currentMove)
            {
                foreach (var tile in t.adjacencyList.Values)
                {
                    if (tile != null && !tile.visited)
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

    public void TryMoveToTile(TileProperties tile)
    {
        if (tile != null && tile.Selectable && tile.CanPlaceUnit(gameObject))
        {
            path.Clear();
            moving = true;
            currentTile.unit = null;
            currentTile.Current = false;
            this.currentTile = tile;
            currentTile.unit = gameObject;

            TileProperties next = tile;
            while (next != null)
            {
                path.Push(next);
                next = next.parent;
            }

            logic.currentMove -= path.Count;
            RemoveSelectableTiles();
        }
        
    }

    public void Move()
    {
        if(path.Count > 0)
        {
            TileProperties t = path.Peek();
            Vector3 target = t.WorldPosForPlacement();
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
