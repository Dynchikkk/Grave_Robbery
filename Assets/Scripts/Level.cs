using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public float timePerLevel;

    [Header("Graves")]
    [Tooltip("Number of graves per level")]
    public int gravesOnLevel;

    [Header("Currency")]
    public int expPerGrave;
    [Range(0, 1)] public float expPerGraveDeviation;
    public int moneyPerGrave;
    [Range(0, 1)] public float moneyPerGraveDeviation;

    [Header("Layers")]
    [Tooltip("Middle grave depth")]
    public int middleGraveDepth;
    [Tooltip("Maximum deviation from average grave depth")]
    [Range(1, 100)] public int graveDepthDeviation;

    [Tooltip("Average HP")]
    public float middleLayerHp;
    //[Tooltip("Deviation of HP from the average value")]
    //[SerializeField, Range(0, 1)] public float layerHpDeviation;

    [Tooltip("Average Resistance")]
    [Range(0, 1)] public float middleLayerResistance;
    //[Tooltip("Deviation of Resistance from the average value")]
    //[SerializeField, Range(0, 1)] public float layerResistanceDeviation;

    [Tooltip("Layer types per level")]
    public List<GameObject> layers = new();

    [Header("Enemies")]
    public List<EnemyOnLevel> enemiesOnLevel = new();

    [Header("Treasures")]
    public int treasurePerLvlCount;
    public List<Treasure> treasuresType = new();
}

[System.Serializable]
public class EnemyOnLevel
{
    public Enemy enemy;
    //public GameObject startPosition;
}
