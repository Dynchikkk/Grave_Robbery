using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOfInteract : MonoBehaviour
{
    private MainLogic _main;
    [SerializeField] private GameObject _interactVisual;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    private void Update()
    {
        CheckIfPlayerInRange();
    }

    private void CheckIfPlayerInRange()
    { 
        if (Physics.OverlapSphere(transform.position, _main.player.InteractionDistance,  1 << 6).Length >= 1)
        {
            _interactVisual.SetActive(true);
            RotateIcon();
        }
        else
            _interactVisual.SetActive(false);
    }

    private void RotateIcon()
    {
        _interactVisual.transform.LookAt(_main.player.cam.transform.position);
    }
}
