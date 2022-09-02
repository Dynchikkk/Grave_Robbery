using System;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class Player : BaseMonoBehaviour
{
    public static Player instance;
    private MainLogic _main;

    public Action<Weapon> OnUseItemAction;
    public event Action OnClickMouseButton;
    public event Action OnJumpInteract;

    [SerializeField] private float _useCd;
    private float _dopUseCd;


    [field: SerializeField] public float InteractionDistance { get; private set; }
    // Sphere radius
    [SerializeField] private float interactionFault;

    [Header("Weapons")]
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _selectedWeapon;


    [Header("Currency")]
    [Header("Experience Points")]
    [SerializeField] private float _expCoefPerLevel;
    [SerializeField] private int _expToLevelUp = 10;
    [SerializeField] private int _expPoints = 0;
    [Header("Money")]
    [SerializeField] private int _playerMoney;
    [field: Header("Tresures")]
    [field: SerializeField] public List<Treasure> PlayerTreasures { get; private set; }

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
        _dopUseCd = _useCd;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnJumpInteract?.Invoke();

        if (Input.GetMouseButtonDown(0))
        {
            UseWeapon();
            Use();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectWeapon(2);

        _dopUseCd -= Time.deltaTime;
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

    public void Use()
    {
        OnClickMouseButton?.Invoke();
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

    public void AddTreasure(Treasure treasure)
    {
        PlayerTreasures.Add(treasure);
    }

    public GameObject CheckIfPlayerSee()
    {
        if (_dopUseCd > 0)
            return null;

        _dopUseCd = _useCd;

        var cameraTransform = Camera.main.transform;
        Vector3 playerPos = cameraTransform.position;
        Vector3 playerLook = cameraTransform.forward;

        if (Physics.Raycast(playerPos, playerLook, out RaycastHit hit, Player.instance.InteractionDistance))
        {
            if (hit.transform.gameObject)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }
}