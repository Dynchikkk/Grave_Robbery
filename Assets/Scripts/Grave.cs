using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public abstract class Grave : BaseMonoBehaviour
{
    [Header("Base Grave Attrubutes")]
    [SerializeField] protected int _exp;
    [SerializeField] protected float _depth;
    [SerializeField] protected List<EarthLayer> _earthLayers = new();
    [SerializeField] protected List<Loot> _loot = new();
    [SerializeField] protected List<GameObject> _layerPrefabs = new();
    [SerializeField] protected GameObject _layerParent;
    [Tooltip("Layer shift by Y")]
    [SerializeField] protected float _shift;

    [Header("Layers Attributes")]
    [Header("Hp")]

    [Tooltip("Average HP")]
    [SerializeField] protected float _middleLayerHp;
    [Tooltip("Deviation of HP from the average value (%)")] 
    [SerializeField, Range(0, 1)] protected float _layerHpDeviation;

    [Header("Resistance")]
    [Tooltip("Average Resistance")] 
    [SerializeField, Range(0, 1)] protected float _middleLayerResistance;
    [Tooltip("Deviation of Resistance from the average value (%)")] 
    [SerializeField, Range(0, 1)] protected float _layerResistanceDeviation;

    protected virtual void OnEnable()
    {
        InstantiateLayers();

        //foreach (EarthLayer layer in _earthLayers)
        //{
        //    layer.Grave = this;
        //    layer.OnEarthLayerDigOut += DigOut;
        //}
    }

    protected void OnDisable()
    {
        foreach (EarthLayer layer in _earthLayers)
        {
            layer.OnEarthLayerDigOut -= DigOut;
        }
    }

    protected virtual void DigOut(EarthLayer earthLayer)
    {
        earthLayer.OnEarthLayerDigOut -= DigOut;
        _earthLayers.Remove(earthLayer);
        Destroy(earthLayer.gameObject);
        if (_earthLayers.Count > 0)
            return;
        Destroy(gameObject);
    }

    protected void InstantiateLayers()
    {
        // Earth width
        float _layerWidth = _layerPrefabs[0].GetComponent<BoxCollider>().size.y / 10;
        float _yShift = 0;

        for (int i = 0; i < _depth; i++)
        {
            // Instantiate
            int layerNum = Random.Range(0, _layerPrefabs.Count);
            EarthLayer newLayer = Instantiate(_layerPrefabs[layerNum], _layerParent.transform).GetComponent<EarthLayer>();

            // Move
            newLayer.transform.localPosition += new Vector3(0, _yShift, 0);
            _yShift -= _layerWidth + _shift;

            // Calculate Parametres
            float hpDevation = _middleLayerHp * _layerHpDeviation;
            float resistanceDevation = _middleLayerResistance * _layerResistanceDeviation;
            newLayer.Health = _middleLayerHp + Random.Range(-hpDevation, hpDevation);
            newLayer.Resistance = _middleLayerResistance + Random.Range(-resistanceDevation, resistanceDevation);

            newLayer.Grave = this;
            newLayer.OnEarthLayerDigOut += DigOut;

            _earthLayers.Add(newLayer);
        }
    }
}
