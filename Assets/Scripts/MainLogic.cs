using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
    public static MainLogic main;

    [Header("Base")]
    public Player player;

    [Header("Graves")]
    public List<Grave> allGraves = new List<Grave>();
    [SerializeField] private List<Grave> _fullGraves = new List<Grave>();

    [Header("Level")]
    [SerializeField] private List<Level> _allLevels = new List<Level>();

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        SetGravesOnLevel(0);
    }

    public void SetGravesOnLevel(int levelNum)
    {
        Level currentLevel = _allLevels[levelNum];

        List<Grave> localGraveList = new();
        for (int i = 0; i < allGraves.Count; i++)
        {
            localGraveList.Add(allGraves[i]);
        }

        int gravesOnLevel = currentLevel.gravesOnLevel;

        for (int i = 0; i < gravesOnLevel; i++)
        {
            int rndGraveNum = Random.Range(0, localGraveList.Count);
            if (localGraveList.Count <= 0 || localGraveList[rndGraveNum] == null)
                return;

            Grave rndGrave = localGraveList[rndGraveNum];
            localGraveList.RemoveAt(rndGraveNum);

            SetGraveCharacteristic(currentLevel, rndGrave);
            rndGrave.InstantiateLayers();
            _fullGraves.Add(rndGrave);
        }

        InstantiateEmptyGraves(localGraveList, currentLevel);
    }

    private void SetGraveCharacteristic(Level curLevel, Grave curGrave)
    {
        curGrave.depth = curLevel.middleGraveDepth + Random.Range(-curLevel.graveDepthDeviation, curLevel.graveDepthDeviation);
        curGrave.middleLayerHp = curLevel.middleLayerHp;
        curGrave.middleLayerResistance = curLevel.middleLayerResistance;
        for (int i = 0; i < curLevel.layers.Count; i++)
        {
            curGrave.layerPrefabs.Add(curLevel.layers[i]);
        }
    }

    private void InstantiateEmptyGraves(List<Grave> emptyGrave, Level curLevel)
    {
        for (int i = 0; i < emptyGrave.Count; i++)
        {
            SetGraveCharacteristic(curLevel, emptyGrave[i]);
            emptyGrave[i].middleLayerHp = 0;
            emptyGrave[i].depth = curLevel.middleGraveDepth;
            emptyGrave[i].InstantiateLayers();
        }
    }
}
