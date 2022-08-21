using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class JumpZone : BaseMonoBehaviour
{
    private Player _player;
    [SerializeField] GameObject _jumpToPosition;

    private void Awake()
    {
        _player = Player.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player.OnJumpInteract += ÑontrolledJump;
    }

    private void OnTriggerExit(Collider other)
    {
        _player.OnJumpInteract -= ÑontrolledJump;
    }

    private void ÑontrolledJump()
    {
        _player.transform.position = _jumpToPosition.transform.position;
    }
}
