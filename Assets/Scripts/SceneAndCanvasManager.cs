using UnityEngine.UI;
using UnityEngine;

public class SceneAndCanvasManager : MonoBehaviour
{
    public void FlipCanvas(Canvas canvasToClose, Canvas canvasToOpen)
    {
        canvasToClose.enabled = false;
        canvasToOpen.enabled = true;
    }
}
