using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    private MainLogic _main;

    private void Awake()
    {
        _main = MainLogic.main;
    }

    public void Win()
    {
        print("win");
        _main.inLocation = false;
    }

    public void Lose()
    {
        print("lose");
        _main.player.GetComponent<FirstPersonMovement>().enabled = false;
        _main.inLocation = false;
    }
}
