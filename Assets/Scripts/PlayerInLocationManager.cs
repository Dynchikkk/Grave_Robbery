using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInLocationManager : MonoBehaviour
{
    private MainLogic _main;

    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameObject _playerStartPosition;
    private Player _player;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    private void OnEnable()
    {
        InstantiatePlayer();
        _player.OnKeyEInteract += MoveFromLocation;
    }

    private void OnDisable()
    {
        _player.OnKeyEInteract -= MoveFromLocation;
    }

    private void MoveFromLocation()
    {
        GameObject curObj = _main.player.CheckIfPlayerSee();
        if (curObj == null)
            return;

        if (curObj.TryGetComponent(out PlayerInLocationManager startPos))
        {
            _main.player.StopPlayer(false);
            print("Win");
        }
    }

    private void InstantiatePlayer()
    {
        Player localPlayer = Instantiate(_playerPrefab, _playerStartPosition.transform);
        localPlayer.transform.SetParent(null);
        _player = localPlayer;
    }
}
