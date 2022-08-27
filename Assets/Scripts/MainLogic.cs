using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;
using UnityEngine.UI;
using TMPro;

public class MainLogic : BaseMonoBehaviour
{
    public static MainLogic main;
    public WinLose winLoose;

    [Header("Base")]
    public Player player;
    public bool inLocation;

    [SerializeField] private float _localTime;
    private float LocalTime
    {
        get => _localTime;
        set
        {
            _localTime = value;
            if (_localTime <= 0)
            {
                winLoose.Lose();
            }
        }
    }

    [Header("Graves")]
    public List<Grave> allGraves = new List<Grave>();
    [SerializeField] private List<Grave> _fullGraves = new List<Grave>();

    [Header("Level")]
    [SerializeField] private List<Level> _allLevels = new List<Level>();

    [Header("UI")]
    [SerializeField] private TMP_Text timeText;

    private void Awake()
    {
        main = this;
        winLoose = GetComponent<WinLose>();
    }

    private void Start()
    {
        SetLevel—haracteristic(0);
    }

    private void Update()
    {
        SetTimeText();

        if (inLocation is true)
        {
            LocalTime -= Time.deltaTime;
        }
    }

    private void SetLevel—haracteristic(int levelNum)
    {
        inLocation = false;
        LocalTime = _allLevels[levelNum].timePerLevel;

        SetGravesOnLevel(levelNum);
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

    public void SetTimeText()
    {
        var time = System.TimeSpan.FromSeconds(LocalTime);
        string timeStr = string.Concat(time.Minutes, ".", time.Seconds, ".", time.Milliseconds);

        if (time.Milliseconds < 0)
            timeStr = "0.0.0";

        timeText.text = timeStr;
    }
}
