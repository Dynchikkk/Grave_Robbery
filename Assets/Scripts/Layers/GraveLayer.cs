using System;
using Project.Architecture;
using UnityEngine;

public abstract class GraveLayer : BaseMonoBehaviour
{
    [field: Header("Base Attribute")]
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

    public Action<GraveLayer> OnEarthLayerDigOut;

    public virtual void TakeDamage(float damage)
    {
        Health -= RecalculateDamage(damage);
        print(Health);
    }

    public virtual float RecalculateDamage(float damage)
    {
        float dam = damage - damage * Resistance;
        return dam;
    }

    protected virtual void DigOut()
    {
        print("Grave excavated");
        OnEarthLayerDigOut?.Invoke(this);
    }

    protected virtual void DigIn()
    {
        print("Grave excavated in progress");
    }
}
