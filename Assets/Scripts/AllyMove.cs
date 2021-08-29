using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMove : Movement
{
    public bool isActive = false;
    private UnitLogic logic;

    void Start()
    {
        logic = gameObject.GetComponent<UnitLogic>();
        move = logic.card.move;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            FindSelectableTiles();
        }
    }

    public void OnClick()
    {
        if(isActive)
        {
            isActive = false;
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
