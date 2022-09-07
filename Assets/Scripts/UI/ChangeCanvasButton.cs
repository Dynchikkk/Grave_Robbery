using UnityEngine;
using UnityEngine.UI;

public class ChangeCanvasButton : MonoBehaviour
{
    [SerializeField] private Canvas _canvasToOpen;
    [SerializeField] private Canvas _canvasToClose;

    public void CanvasChange()
    {
        MainLogic.main.sceneAndCanvasManager.FlipCanvas(_canvasToClose, _canvasToOpen);
    }
}
