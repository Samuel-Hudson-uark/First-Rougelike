using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMove : Movement
{
    public bool isActive = false;
    private MouseHandler mouseHandler;

    private void Start()
    {
        mouseHandler = GameObject.Find("GameEngine").GetComponent<MouseHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!turn)
            return;

        if(isActive)
        {
            if(!moving)
            {
                FindSelectableTiles();
                CheckMouse();
            }
        }
        if (moving)
            Move();
    }

    void CheckMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            TileProperties t = GetTileManager().findTile(mouseHandler.hoveringPos);
            if(t != null && t.selectable)
            {
                MoveToTile(t);
                isActive = false;
            }
        }
    }

    public void OnClick()
    {
        if (isActive)
        {
            isActive = false;
            RemoveSelectableTiles();
        } else
        {
            foreach(GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
            {
                ally.GetComponent<AllyMove>().isActive = false;
            }
            isActive = true;
        }
    }
}
