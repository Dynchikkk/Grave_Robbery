using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Inventory")]
    public BaseShovel playerShovel;

    [Header("Interaction attribute")]
    public int interactionDistance;
    // Sphere radius
    public float interactionFault;

    [Header("Another")]
    public Camera playerCamera;
    public static Player main;

    private void Awake()
    {
        main = this;
        // Временно
        playerShovel.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RightMouseClick();
        }
    }

    public void Interact()
    {
        RaycastHit hit;
        Vector3 playerPos = playerCamera.transform.position;
        Vector3 playerLook = playerCamera.transform.forward;

        if (Physics.SphereCast(playerPos, interactionFault, playerLook, out hit, interactionDistance))
        {
            print(hit.transform.gameObject.name);

            if (hit.transform.gameObject.TryGetComponent(out LightSwitch switcher))
            {
                switcher.SwitchLightCondition();
            }
        }
    }

    public event Action OnRightMouseClick;
    public void RightMouseClick()
    {
        if (OnRightMouseClick != null)
        {
            OnRightMouseClick();
        }
    }
}