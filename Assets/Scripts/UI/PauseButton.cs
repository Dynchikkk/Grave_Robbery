using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void SetPause()
    {
        MainLogic.main.NoPause(!MainLogic.main.noPause);
    }
}
