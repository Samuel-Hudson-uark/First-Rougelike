using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Tile> adjacencyList = new List<Tile>();

    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    // Update is called once per frame
    void Update()
    {
        if(current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public bool CanPass(GameObject unit)
    {
        return true;
    }

    public void Reset()
    {
        adjacencyList.Clear();

        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors()
    {
        Reset();
        CheckTile(Vector2.up);
        CheckTile(Vector2.down);
        CheckTile(Vector2.right);
        CheckTile(Vector2.left);

    }

    public void CheckTile(Vector2 direction)
    {
        Vector2 halfExtents = new Vector2(0.25f, 0.25f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) + direction, halfExtents, 0);

        foreach(Collider2D item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile != null && true && tile.transform.childCount == 0) //TODO: check if tile is walkable
            {
                adjacencyList.Add(tile);
            }
        }
    }

    public void OnClick()
    {
        Debug.Log("Click");

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            tile.GetComponent<Tile>().Reset();
        }
            if (transform.childCount > 0)
        {
            AllyMove move = transform.GetChild(0).gameObject.GetComponent<AllyMove>();
            if (move != null)
            {
                move.OnClick();
            }
        }
    }
}
