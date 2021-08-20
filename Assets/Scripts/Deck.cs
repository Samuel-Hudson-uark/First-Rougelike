using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private Stack<MonsterCard> cards;
    public MonsterCard tempCard;
    public MonsterCard tempCard2;
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
        cards = Shuffle(cards);
    }

    public void OnClick()
    {
        if(cards.Count > 0)
        {
            GameObject playerCard = Instantiate(cardObject, new Vector3(0,0,0), Quaternion.identity);
            playerCard.GetComponent<CardDisplay>().card = cards.Pop();
            playerCard.transform.SetParent(handObject.transform, false);
        }
    }

    private Stack<T> Shuffle<T>(Stack<T> stack)
    {
        return new Stack<T>(stack.OrderBy(x => Random.Range(0, stack.Count-1)));
    }
}
