using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMove : Movement
{
    public bool isActive = false;

    void Start()
    {
        Init();    
    }

    // Update is called once per frame
    void Update()
    {
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
            {
                Tile t = hit.collider.gameObject.GetComponent<Tile>();
                if(t.selectable)
                {
                    MoveToTile(t);
                    isActive = false;
                }
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
