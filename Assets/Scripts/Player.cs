using System;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class Player : BaseMonoBehaviour
{
    public static Player instance;
    private MainLogic _main;

    public Action<Weapon> OnUseItemAction;
    //public event Action OnClickMouseButton;
    public event Action OnKeyEInteract;
    public event Action OnJumpInteract;

    [SerializeField] private float _useCd;
    private float _dopUseCd;

    [field: SerializeField] public float InteractionDistance { get; private set; }
    // Sphere radius
    [SerializeField] private float interactionFault;

    [Header("Weapons")]
    public List<Weapon> weapons = new List<Weapon>(4);
    [SerializeField] private Weapon _selectedWeapon;
    [SerializeField] private GameObject _inventoryParent;


    [Header("Currency")]
    [Header("Experience Points")]
    [SerializeField] private float _expCoefPerLevel;
    [SerializeField] private int _expToLevelUp = 10;
    [SerializeField] private int _expPoints = 0;
    [Header("Money")]
    [SerializeField] private int _playerMoney;
    [Header("Tresures")]
    public List<Treasure> playerTreasures = new List<Treasure>(5);

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

        _dopUseCd = _useCd;

        for (int i = 0; i < weapons.Count; i++)
        {
            InstantiateItem(i);
        }
        SelectDefaultWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnJumpInteract?.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            OnKeyEInteract?.Invoke();

        if (Input.GetMouseButtonDown(0))
            UseWeapon();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectWeapon(3);

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

    private void UseWeapon()
    {
        print("use weapon");
        OnUseItemAction?.Invoke(_selectedWeapon);
    }

    private void SelectWeapon(int index)
    {
        if (weapons.Count <= index || weapons[index] is null)
            return;
        if (_selectedWeapon != null)
            _selectedWeapon.gameObject.SetActive(false);
        _selectedWeapon = weapons[index];
        _selectedWeapon.gameObject.SetActive(true);
    }

    public void SelectDefaultWeapon()
    {
        foreach (var weapon in weapons)
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
        if (playerTreasures.Contains(null) is false)
        {
            print("Not enough place");
            return;
        }

        for (int i = 0; i < playerTreasures.Count; i++)
        {
            if (playerTreasures[i] == null)
            {
                playerTreasures[i] = treasure;
                break;
            }
        }
    }

    public GameObject CheckIfPlayerSee()
    {
        //if (_dopUseCd > 0)
        //    return null;

        //_dopUseCd = _useCd;

        var cameraTransform = Camera.main.transform;
        Vector3 playerPos = cameraTransform.position;
        Vector3 playerLook = cameraTransform.forward;

        if (Physics.Raycast(playerPos, playerLook, out RaycastHit hit, InteractionDistance))
        {
            if (hit.transform.gameObject)
            {
                print(hit.transform.gameObject.name);
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    public void StopPlayer(bool condition)
    {
        GetComponent<FirstPersonMovement>().enabled = condition;
        GetComponent<Jump>().enabled = condition;
        GetComponent<Crouch>().enabled = condition;
        GetComponentInChildren<FirstPersonLook>().enabled = condition;
    }

    public void RemoveSelectedWeapon()
    {
        if (_selectedWeapon == null)
            return;

        _selectedWeapon.gameObject.SetActive(false);
        _selectedWeapon = null;
    }

    public bool SetItem(Item item)
    {
        if (weapons.Contains(null) == false)
        {
            print("Not enough place");
            return false;
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = (Weapon)item;
                _main.money -= item.Cost;
                InstantiateItem(i);
                break;
            }
        }

        return true;
    }

    public void InstantiateItem(int numInWeaponList)
    {
        if (weapons[numInWeaponList] == null)
            return;

        GameObject localItem = Instantiate(weapons[numInWeaponList].gameObject, _inventoryParent.transform);
        localItem.SetActive(false);

        weapons[numInWeaponList] = localItem.GetComponent<Weapon>();
    }
}