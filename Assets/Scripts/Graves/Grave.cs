using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public abstract class Grave : BaseMonoBehaviour
{
    private MainLogic _main;

    [Header("Base Grave Attrubutes")]
    public int money;
    [Tooltip("Deviation of money from the average value (%)")]
    [Range(0, 1)] public float moneyDeviation;
    public int exp;
    [Tooltip("Deviation of exp from the average value (%)")]
    [Range(0, 1)] public float expDeviation;
    public int depth;
    [SerializeField] protected List<GraveLayer> _earthLayers = new();
    [SerializeField] protected List<Loot> _loot = new();
    public List<GameObject> layerPrefabs = new();
    [SerializeField] protected GameObject _layerParent;
    [Tooltip("Layer shift by Y")]
    [SerializeField] protected float _shift;
    [SerializeField] protected GameObject _floor;
    [SerializeField] protected GameObject _wall;

    [Header("Layers Attributes")]
    [Header("Hp")]

    [Tooltip("Average HP")]
    public float middleLayerHp;
    [Tooltip("Deviation of HP from the average value (%)")] 
    [SerializeField, Range(0, 1)] protected float _layerHpDeviation;

    [Header("Resistance")]
    [Tooltip("Average Resistance")] 
    [Range(0, 1)] public float middleLayerResistance;
    [Tooltip("Deviation of Resistance from the average value (%)")] 
    [SerializeField, Range(0, 1)] protected float _layerResistanceDeviation;

    [Header("Controlled Jump")]
    [SerializeField] JumpZone _jumpStartPosition;
    [SerializeField] GameObject _jumpEndPosition;

    protected virtual void OnEnable()
    {
        _main = MainLogic.main;
        _main.allGraves.Add(this);
    }

    protected void Start()
    {
        SetStartCharaceristic();
    }

    protected void OnDisable()
    {
        foreach (GraveLayer layer in _earthLayers)
        {
            layer.OnEarthLayerDigOut -= DigOut;
        }
    }

    protected void SetStartCharaceristic()
    {
        exp += System.Convert.ToInt32(Random.Range(-exp * expDeviation, exp * expDeviation));
        money += System.Convert.ToInt32(Random.Range(-money * moneyDeviation, money * moneyDeviation));
    }

    protected virtual void DigOut(GraveLayer earthLayer)
    {
        earthLayer.OnEarthLayerDigOut -= DigOut;
        _earthLayers.Remove(earthLayer);
        Destroy(earthLayer.gameObject);
        if (_earthLayers.Count > 0)
            return;
        _main.player.AddExpPoints(exp);
        _main.player.SetLocalMoney(money);
        print("grave Destroy");
        //Destroy(gameObject);
    }

    public void InstantiateLayers()
    {
        float _yShift = 0;

        for (int i = 0; i < depth; i++)
        {
            // Instantiate
            int layerNum = Random.Range(0, layerPrefabs.Count);
            GraveLayer newLayer = Instantiate(layerPrefabs[layerNum], _layerParent.transform).GetComponent<GraveLayer>();

            newLayer.Grave = this;
            newLayer.OnEarthLayerDigOut += DigOut;

            // Move
            newLayer.transform.localPosition += new Vector3(0, _yShift, 0);
            _yShift -= _shift;

            // Calculate Parametres
            float hpDevation = Mathf.Round(middleLayerHp * _layerHpDeviation);
            float resistanceDevation = middleLayerResistance * _layerResistanceDeviation;
            newLayer.Health = middleLayerHp + Random.Range(-hpDevation, hpDevation);
            newLayer.Resistance = middleLayerResistance + Random.Range(-resistanceDevation, resistanceDevation);

            _earthLayers.Add(newLayer);
        }

        InstantiateWallsAndFloor();
        SetJumpZone();
    }

    protected void InstantiateWallsAndFloor()
    {
        for (int i = 0; i < depth; i++)
        {
            GameObject wall = Instantiate(_wall, _earthLayers[i].transform);
            wall.transform.parent = _earthLayers[i].transform.parent.parent;
            foreach (var mat in wall.GetComponentsInChildren<MeshRenderer>())
                mat.material = _earthLayers[i].GetComponent<MeshRenderer>().material;

            // Временно
            wall.transform.localScale = new Vector3(1, 1, 1);
        }

        int floorNum = depth - 1;

        GameObject floor = Instantiate(_floor, _earthLayers[floorNum].transform);
        floor.transform.parent = _earthLayers[floorNum].transform.parent.parent;
        floor.GetComponentInChildren<MeshRenderer>().material = _earthLayers[floorNum].GetComponent<MeshRenderer>().material;
        // Временно
        floor.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetJumpZone()
    {
        _jumpStartPosition.JumpToPosition = _jumpEndPosition;

        // Calculate grave depth
        float graveDepth = depth * _shift;
        float middleOfGrave = graveDepth / 2;

        // Replace JumpZone
        _jumpStartPosition.transform.localScale = new Vector3(_jumpStartPosition.transform.localScale.x, graveDepth, layerPrefabs[0].transform.localScale.z);
        _jumpStartPosition.gameObject.transform.localPosition = new Vector3(_jumpStartPosition.gameObject.transform.localPosition.x, -middleOfGrave,
            _jumpStartPosition.gameObject.transform.localPosition.z);
    }
}
