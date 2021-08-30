using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<Movement>> units = new Dictionary<string, List<Movement>>();
    static Queue<string> turnKey = new Queue<string>();
    static List<Movement> teamList = new List<Movement>();

    void Update()
    {
        if(teamList.Count == 0 && units.Count != 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        teamList = units[turnKey.Peek()];
    }

    public static void StartTurn()
    {
        Debug.Log("Begin Turn");
        //start of turn actions (i.e. refresh mana and moves and attacks)
        foreach(Movement move in teamList)
        {
            move.BeginTurn();
        }
    }

    public static void EndTurn()
    {
        //end of turn actions
        foreach (Movement move in teamList)
            move.EndTurn();
        
        if(turnKey.Count > 0)
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
        StartTurn();
    }

    public static void AddUnit(Movement unit)
    {
        List<Movement> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<Movement>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
        if(unit.tag == turnKey.Peek())
        {
            unit.BeginTurn();
        }
    }
}
