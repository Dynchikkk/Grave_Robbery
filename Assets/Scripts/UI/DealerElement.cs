 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerElement : MonoBehaviour
{
    private MainLogic _main;

    public int numInPlayerList;
    public Item objectToBuy;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    public void Buy()
    {
        _main.currentDealer.Buy(objectToBuy);
    }

    public void SellItem()
    {
        _main.currentDealer.SellItem(numInPlayerList);
    }

    public void SellTreasure()
    {
        _main.currentDealer.SellTreasure(numInPlayerList);
    }
}
