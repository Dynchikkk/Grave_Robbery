using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerElement : MonoBehaviour
{
    private MainLogic _main;

    [Header("Base")]
    public int numInPlayerList;
    public Item objectToBuy;

    [Header("UI")]
    [SerializeField] private Button actionButton;
    [SerializeField] private Image itemIcon;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    public void SetIcon(Sprite img)
    {
        itemIcon.sprite = img;
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
