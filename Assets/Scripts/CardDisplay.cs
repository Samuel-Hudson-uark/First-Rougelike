using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public MonsterCard card;
    public MonsterCard originalCard;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    
    public Text costText;
    public Text moveText;
    public Text attackText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        card = Instantiate(originalCard);
        OnAttributesChanged();
    }

    void OnAttributesChanged()
    {
        nameText.text = card.name;
        descriptionText.text = card.text;

        artworkImage.sprite = card.artwork;

        costText.text = card.cost.ToString();
        moveText.text = card.move.ToString();
        attackText.text = card.attack.ToString();
        healthText.text = card.health.ToString();
    }

    public void ChangeCard(MonsterCard card)
    {
        this.card = card;
        OnAttributesChanged();
    }
}
