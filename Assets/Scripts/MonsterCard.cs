using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class MonsterCard : ScriptableObject
{
    public new string name;
    public string text;

    public Sprite artwork;

    public int cost;
    public int move;
    public int attack;
    public int health;

    public GameObject unit;

    public void Print()
    {
        Debug.Log(name);
    }
}
