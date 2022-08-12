using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Interaction attribute")]
    public int interactionDistance;
    // Sphere radius
    public float interactionFault;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
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
}