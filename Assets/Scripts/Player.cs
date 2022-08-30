using System;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class Player : BaseMonoBehaviour
{
    public static Player instance;
    private MainLogic _main;

    public Action<Weapon> OnUseItemAction;
    public event Action OnJumpInteract;

    [Header("Weapons")]
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _selectedWeapon;

    [Header("Interaction attribute")]
    [SerializeField] private int interactionDistance;
    // Sphere radius
    [SerializeField] private float interactionFault;

    [Header("Currency")]
    [Header("Experience Points")]
    [SerializeField] private float _expCoefPerLevel;
    [SerializeField] private int _expToLevelUp = 10;
    [SerializeField] private int _expPoints = 0;
    [Header("Money")]
    [SerializeField] private int _playerMoney;

    public int ExpPoints
    {
        get => _expPoints;
        set
        {
            _expPoints = value;
            if (_expPoints >= _expToLevelUp)
            {
                UpLevel();
            }
        }
    }

    [Header("Another")]
    public int playerLevel = 1;
    public Camera cam;
    
    private void OnEnable()
    {
        instance = this;
        _main = MainLogic.main;

        SelectDefaultWeapon();  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnJumpInteract?.Invoke();
        
        if (Input.GetMouseButtonDown(0))
            UseWeapon();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectWeapon(2);
    }

    public void UpLevel()
    {
        int lastCoef = _expToLevelUp;
        _expToLevelUp = Convert.ToInt32(float.Parse(_expToLevelUp.ToString()) * _expCoefPerLevel);
        playerLevel += 1;
        ExpPoints -= lastCoef;
    }

    public void AddExpPoints(int exp)
    {
        print("You earn " + exp.ToString() + "exp");
        ExpPoints += exp;
    }

    private void UseWeapon()
    {
        print("use weapon");
        OnUseItemAction?.Invoke(_selectedWeapon);
    }

    private void SelectWeapon(int index)
    {
        if (_weapons.Count <= index || _weapons[index] is null)
            return;
        _selectedWeapon.gameObject.SetActive(false);
        _selectedWeapon = _weapons[index];
        _selectedWeapon.gameObject.SetActive(true);
    }

    private void SelectDefaultWeapon()
    {
        foreach (var weapon in _weapons)
            weapon?.gameObject.SetActive(false);
        SelectWeapon(0);
    }

    public void SetLocalMoney(int value)
    {
        _playerMoney += value;
    }

    public void SetLocalToGlobalMoney()
    {
        _main.money += _playerMoney;
        _playerMoney = 0;
    }
}