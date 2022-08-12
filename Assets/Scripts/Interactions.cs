using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// maybe
public class Interactions : MonoBehaviour
{
    public static Interactions main;

    private void Awake()
    {
        main = this;
    }

    public event Action onKeyEPressed;
    public void Interaction()
    {
        if (onKeyEPressed != null)
        {
            onKeyEPressed();
        }
    }
}
