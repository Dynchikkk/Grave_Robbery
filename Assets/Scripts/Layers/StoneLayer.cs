using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLayer : EarthLayer
{
    [Header("Stone Layer Attribute")]
    [SerializeField] private float _hardness—oefficient;

    private void Start()
    {
        Health *= _hardness—oefficient;
    }
}
