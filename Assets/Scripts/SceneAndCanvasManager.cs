using UnityEngine.UI;
using UnityEngine;

public class SceneAndCanvasManager : MonoBehaviour
{
    public void FlipCanvas(Canvas canvasToClose, Canvas canvasToOpen)
    {
        canvasToClose.gameObject.SetActive(false);
        canvasToOpen.gameObject.SetActive(true);
    }
}
