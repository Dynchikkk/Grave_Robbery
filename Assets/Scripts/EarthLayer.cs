using System;
using Project.Architecture;
using UnityEngine;

public abstract class EarthLayer : BaseMonoBehaviour
{
    [field: SerializeField] public Grave Grave { get; set; }
    [SerializeField] private float _health;

    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
                DigOut();
            else
                DigIn();
        }
    }

    [field: SerializeField, Range(0,1)] public float Resistance { get; set; }
    [field: SerializeField] public float Level { get; protected set; }

    public Action<EarthLayer> OnEarthLayerDigOut;

    public virtual void TakeDamage(float damage)
    {
        Health -= RecalculateDamage(damage);
        print(Health);
    }

    protected virtual float RecalculateDamage(float damage)
    {
        return damage - damage * Resistance;
    }

    protected virtual void DigOut()
    {
        print("Grave excavated");
        OnEarthLayerDigOut?.Invoke(this);
    }

    protected virtual void DigIn()
    {
        print("Grave excavated");
    }
}
