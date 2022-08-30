using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _allMoney;
    public int AllMoney
    {
        get => _allMoney;
        set
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

    public void SetMoney(int value)
    {
        AllMoney += value;
    }
}
