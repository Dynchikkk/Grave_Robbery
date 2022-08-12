using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<GameObject> lights = new List<GameObject>();

    public void SwitchLightCondition()
    {
        foreach (var light in lights)
        {
            light.SetActive(!light.activeSelf);
        }
    }
}
