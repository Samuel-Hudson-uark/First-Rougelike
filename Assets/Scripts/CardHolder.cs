using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public Stack<MonsterCard> cards;

    public Stack<T> Shuffle<T>(Stack<T> stack)
    {
        return new Stack<T>(stack.OrderBy(x => Random.Range(0, stack.Count-1)));
    }
}
