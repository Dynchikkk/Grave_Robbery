using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public abstract class Grave : BaseMonoBehaviour
{
    [SerializeField] protected int _exp;
    [SerializeField] protected float _depth;
    [SerializeField] protected List<EarthLayer> _earthLayers = new();
    [SerializeField] protected List<Loot> _loot = new();


    protected virtual void OnEnable()
    {
        foreach (EarthLayer layer in _earthLayers)
        {
            layer.Grave = this;
            layer.OnEarthLayerDigOut += DigOut;
        }
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
}
