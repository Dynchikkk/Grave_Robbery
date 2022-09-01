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
        _main.player.SetLocalToGlobalMoney();
    }

    public void Lose()
    {
        print("lose");
        _main.player.GetComponent<FirstPersonMovement>().enabled = false;
        _main.inLocation = false;
    }
}