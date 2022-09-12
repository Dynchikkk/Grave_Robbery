using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerUI : MonoBehaviour
{
    private MainLogic _main;

    [Header("Elements Prefab")]
    [SerializeField] private GameObject buyPrefab;
    [SerializeField] private GameObject sellPrefab;
    [SerializeField] private GameObject sellTreasurePrefab;

    [Header("Parents")]
    [SerializeField] GameObject buyParent;
    [SerializeField] GameObject sellParent;
    [SerializeField] GameObject sellTreasureParent;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    private void Start()
    {
        //Временно, потом по кнопке
        InstantiateAllElements();
    }

    private void OnEnable()
    {
        _main.currentDealer.OnSellEvent += InstantiateBuyElement;
        _main.currentDealer.OnBuyEvent += InstantiateSellElement;
        _main.currentDealer.OnInteract += InstantiateAllElements;
    }

    private void OnDisable()
    {
        _main.currentDealer.OnSellEvent -= InstantiateBuyElement;
        _main.currentDealer.OnBuyEvent -= InstantiateSellElement;
        _main.currentDealer.OnInteract -= InstantiateAllElements;
    }

    public void InstantiateAllElements()
    {
        InstantiateSellElements();
        InstantiateSellTreasureElements();
        InstantiateBuyElements();
    }

    private void InstantiateSellElements()
    {
        DestroyAllChilds(sellParent);

        for (int i = 0; i < _main.player.weapons.Count; i++)
        {
            InstantiateSellElement(i);
        }
    }

    private void InstantiateSellTreasureElements()
    {
        DestroyAllChilds(sellTreasureParent);

        for (int i = 0; i < _main.player.playerTreasures.Count; i++)
        {
            if (_main.player.playerTreasures[i] == null)
                continue;

            GameObject sellTreasureElement = Instantiate(sellTreasurePrefab, sellTreasureParent.transform);
            DealerElement sellTreasureElementScript = sellTreasureElement.GetComponent<DealerElement>();
            sellTreasureElementScript.numInPlayerList = i;
            sellTreasureElementScript.SetIcon(_main.player.playerTreasures[i].icon);
        }
    }

    private void InstantiateBuyElements()
    {
        DestroyAllChilds(buyParent);

        for (int i = 0; i < _main.currentDealer.salesSheet.Count; i++)
        {
            InstantiateBuyElement(i);
        }
    }

    public void InstantiateBuyElement(int numInDealerList)
    {
        if (_main.currentDealer.salesSheet[numInDealerList] == null)
            return;

        GameObject buyElement = Instantiate(buyPrefab, buyParent.transform);
        DealerElement buyElementScript = buyElement.GetComponent<DealerElement>();
        buyElementScript.numInDealerList = numInDealerList;
        buyElementScript.SetIcon(_main.currentDealer.salesSheet[numInDealerList].icon);
    }

    public void InstantiateSellElement(int numInPlayerList)
    {
        if (_main.player.weapons[numInPlayerList] == null)
            return;

        GameObject sellElement = Instantiate(sellPrefab, sellParent.transform);
        DealerElement sellElementScript = sellElement.GetComponent<DealerElement>();
        sellElementScript.numInPlayerList = numInPlayerList;
        sellElementScript.SetIcon(_main.player.weapons[numInPlayerList].icon);
    }

    private void DestroyAllChilds(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

    public void ChangeMagazine(bool toItemMagazine)
    {
        sellParent.SetActive(toItemMagazine);
        sellTreasureParent.SetActive(!toItemMagazine);
    }
}
