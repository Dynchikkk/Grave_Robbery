using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject light;

    public void SwitchLightCondition()
    {
        light.SetActive(!light.activeSelf);
    }
}
