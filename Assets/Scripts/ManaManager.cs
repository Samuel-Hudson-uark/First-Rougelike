using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField]
    private int manaGainPerTurn;
    private int currentMana;
    public int CurrentMana {
        get => currentMana;
        set {
            currentMana = value;
            manaNumber.text = currentMana.ToString();
        }
    }

    [SerializeField]
    private Text manaNumber;

    private void Start()
    {
        RegainMana();
    }

    public void RegainMana()
    {
        CurrentMana = manaGainPerTurn;
    }

    public bool CanAfford(int cost)
    {
        return CurrentMana >= cost;
    }

    public bool SpendMana(int cost)
    {
        if(CanAfford(cost))
        {
            CurrentMana -= cost;
            return true;
        }
        return false;
    }


}
