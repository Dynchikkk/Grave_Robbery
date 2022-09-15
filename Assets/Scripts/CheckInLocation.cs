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
            _main.inLocation = true;
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
