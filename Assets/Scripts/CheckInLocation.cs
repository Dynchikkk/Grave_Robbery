using UnityEngine;
using Project.Architecture;

public class CheckInLocation : BaseMonoBehaviour
{
    private MainLogic _main;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (_main.inLocation is false)
        {
            _main.inLocation = true;
            _main.player.SetLocalMoney(0);
        }
        else
            CheckWherePlayer();   
    }

    private void CheckWherePlayer()
    {
        if (_main.inLocation is true)
        {
            _main.inLocation = false;
            _main.winLoose.Win();
        }
    }
}
