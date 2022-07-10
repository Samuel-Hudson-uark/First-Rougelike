using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLogic : MonoBehaviour
{
    public MonsterCard originalCard;
    public MonsterCard card;
    public int currentMove;
    public int currentAttacks;
    public int maxAttacks;

    public int totalDamage = 0;

    public bool attacking = false;

    public void Init()
    {
        card = Instantiate(originalCard);
        maxAttacks = 1;
    }

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
        currentMove = card.move;
    }

    public bool CanMove()
    {
        return currentMove == card.move;
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
