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

    public void InstantiateAllElements()
    {
        InstantiateSellElements();
        InstantiateSellTreasureElements();
        InstantiateBuyElements();
    }

    public void InstantiateSellElements()
    {
        for (int i = 0; i < _main.player.weapons.Count; i++)
        {
            if (_main.player.weapons[i] == null)
                continue;
            
            GameObject sellElement = Instantiate(sellPrefab, sellParent.transform);
            DealerElement sellElementScript = sellElement.GetComponent<DealerElement>();
            sellElementScript.numInPlayerList = i;
            sellElementScript.SetIcon(_main.player.weapons[i].icon);
        }
    }

    public void InstantiateSellTreasureElements()
    {
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

    public void InstantiateBuyElements()
    {
        for (int i = 0; i < _main.currentDealer.salesSheet.Count; i++)
        {
            GameObject buyElement = Instantiate(buyPrefab, buyParent.transform);
            DealerElement buyElementScript = buyElement.GetComponent<DealerElement>();
            buyElementScript.numInPlayerList = i;
            buyElementScript.SetIcon(_main.currentDealer.salesSheet[i].icon);
        }
    }
}
