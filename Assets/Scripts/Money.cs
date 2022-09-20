using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _allMoney;
    public int AllMoney
    {
        get => _allMoney;
        private set
        {
            if (value < 0)
            {
                print("Not enough money");
            }
            else
            {
                _allMoney = value;
            } 
        }
    }

    private void Start()
    {
        SetMoney(0);
    }

    public void SetMoney(int value)
    {
        AllMoney += value;
        MainLogic.main.valuesUI.SetMoneyText(value);
    }
}
