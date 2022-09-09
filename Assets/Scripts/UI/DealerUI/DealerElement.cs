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
        bool needToDestoy = _main.currentDealer.Buy(objectToBuy);
        if (needToDestoy)
            Destroy(gameObject);
    }

    public void SellItem()
    {
        _main.currentDealer.SellItem(numInPlayerList);
        Destroy(gameObject);
    }

    public void SellTreasure()
    {
        _main.currentDealer.SellTreasure(numInPlayerList);
        Destroy(gameObject);
    }
}
