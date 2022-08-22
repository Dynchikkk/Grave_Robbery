using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class JumpZone : BaseMonoBehaviour
{
    private Player _player;
    [field: SerializeField] public GameObject JumpToPosition { private get; set; }

    private void Awake()
    {
        _player = Player.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player.OnJumpInteract += �ontrolledJump;
    }

    private void OnTriggerExit(Collider other)
    {
        _player.OnJumpInteract -= �ontrolledJump;
    }

    private void �ontrolledJump()
    {
        _player.transform.position = JumpToPosition.transform.position;
    }
}
