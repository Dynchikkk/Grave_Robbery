using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShovel : MonoBehaviour
{
    public ShovelAttribute shovelAttribute;

    protected Player _player;

    private void Awake()
    {
        _player = Player.main;
    }

    private void OnEnable()
    {
        Player.main.OnRightMouseClick += Dig;
    }

    private void OnDisable()
    {
        Player.main.OnRightMouseClick -= Dig;
    }

    public void Dig()
    {
        print("dig");
    }
}

[System.Serializable]
public class ShovelAttribute
{
    public string name;
    public float digDamage;
    public float digSpeed;
}
