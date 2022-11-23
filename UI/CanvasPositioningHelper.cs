using UnityEngine;

public class CanvasPositioningHelper : MonoBehaviour
{
    public static Vector2 ScreenPointToRect(Vector2 screenPosition, Canvas canvas)
    {
        Rect rect = canvas.GetComponent<RectTransform>().rect;
        Vector2 canvasPosition = new Vector2(screenPosition.x / Screen.width * rect.width, screenPosition.y / Screen.height * rect.height);
        return canvasPosition;
    }
}
