using System.Collections;
using System.Collections.Generic;
using Project.Architecture;
using UnityEngine;

public class Enemy : BaseMonoBehaviour
{
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewDistanse;

    private Player _player;
    private void Awake()
    {
        _player = Player.instance;
    }

    public void CheckPlayerInFieldOfVision()
    {
        if (Vector3.Angle(transform.forward, _player.transform.position - transform.position) <= _viewAngle)
        {
            RaycastHit hit;
            Vector3 playerPos = _player.cam.transform.position;
            Vector3 playerLook = _player.cam.transform.forward;

            if (Physics.Raycast(playerPos, playerLook, out hit, _viewDistanse))
            {
                if (hit.transform.CompareTag("Player") is false)
                    return;

                print("see player");
            }
        }
    }
}
