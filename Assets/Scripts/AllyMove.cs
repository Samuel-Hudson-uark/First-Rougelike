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
        /*
        if (!turn)
            return;
        if(isActive)
        {
            if(!moving)
            {
                FindSelectableTiles();
            }
        }
        */
        if (moving)
            Move();
    }

    public void OnClick()
    {
        if (moving)
            return;
        if (isActive)
        {
            TileProperties t = TileManager.GetTileAt(mouseHandler.hoveringPos);
            TryMoveToTile(t);
            isActive = false;
            RemoveSelectableTiles();
        } else
        {
            foreach(GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
            {
                ally.GetComponent<AllyMove>().isActive = false;
            }
            if(logic.CanMove())
                isActive = true;
            FindSelectableTiles();
        }
    }
}
