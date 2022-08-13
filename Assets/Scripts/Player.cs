using System;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class Player : BaseMonoBehaviour
{
    public Action<Weapon> OnUseItemAction;

    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Weapon _selectedWeapon;

    [Header("Interaction attribute")]
    [SerializeField] private int interactionDistance;
    // Sphere radius
    [SerializeField] private float interactionFault;

    [Header("Another")]
    [SerializeField] private Camera _camera;
    
    private void OnEnable()
    {
        SelectDefaultWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Interact();
        }

        if (Input.GetMouseButtonDown(0))
        {
            UseWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectWeapon(2);
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
}