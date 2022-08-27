using UnityEngine;
using Project.Architecture;

public class CheckInLocation : BaseMonoBehaviour
{
    private MainLogic _main;
    private bool _firstTime = true;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_firstTime)
        {
            _main.inLocation = true;
            _firstTime = false;
        }
        else
            CheckWherePlayer();   
    }

    private void CheckWherePlayer()
    {
        if (_main.inLocation is true)
        {
            _main.winLoose.Win();
        }
    }
}
