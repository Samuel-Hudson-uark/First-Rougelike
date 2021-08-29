using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public void EndTurn()
    {
        //end of turn actions
    }

    public void StartTurn()
    {
        //start of turn actions (i.e. refresh mana and moves and attacks)
        foreach(GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
        {
            UnitLogic logic = ally.GetComponent<UnitLogic>();
        }
    }

    public void OnClick()
    {
        EndTurn();
        //TODO: pass turn over to opponent
        StartTurn();
    }
}
