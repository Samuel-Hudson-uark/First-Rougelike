using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLogic : MonoBehaviour
{
    public MonsterCard originalCard;
    private MonsterCard card;
    public MonsterCard Card 
    { 
        get 
        { 
            if(card == null)
                card = Instantiate(originalCard);
            return card;
        } 
    }
    public int currentMove;
    public int currentAttacks;
    public int maxAttacks = 1;

    public int totalDamage = 0;

    public bool attacking = false;

    void Update()
    {
        if (totalDamage >= card.health)
        {
            Die();
        }
    }

    public void RefreshAttack()
    {
        currentAttacks = maxAttacks;
    }
    public void RefreshMove()
    {
        currentMove = Card.move;
    }

    public bool CanMove()
    {
        return currentMove == Card.move;
    }

    public bool CanAttack()
    {
        return currentAttacks > 0;
    }

    public void TakeDamage(int damage)
    {
        totalDamage += damage;
    }
    public void RestoreHealth(int healing)
    {
        totalDamage -= healing;
        if (totalDamage < 0)
            totalDamage = 0;
    }
    public void Die()
    {
        GetComponent<Animator>().SetTrigger("die");
    }
}
