using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardHolder
{
    public MonsterCard tempCard;
    public MonsterCard tempCard2;
    public MonsterCard tempCard3;
    public GameObject cardObject;
    public GameObject handObject;

    // Start is called before the first frame update
    void Start()
    {
        cards = new Stack<MonsterCard>();
        cards.Push(Instantiate(tempCard));
        cards.Push(Instantiate(tempCard));
        cards.Push(Instantiate(tempCard));
        cards.Push(Instantiate(tempCard));
        cards.Push(Instantiate(tempCard));
        cards.Push(Instantiate(tempCard2));
        cards.Push(Instantiate(tempCard3));
        cards = Shuffle(cards);
    }

    public void OnClick()
    {
        if (cards.Count > 0)
        {
            GameObject playerCard = Instantiate(cardObject, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.GetComponent<CardDisplay>().originalCard = cards.Pop();
            playerCard.transform.SetParent(handObject.transform, false);
        }
    }
}
