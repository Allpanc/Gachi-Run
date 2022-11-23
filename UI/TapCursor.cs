using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]

public class TapCursor : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    private Image _image;
    private RectTransform _rectTransform;

    private void Start()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    public void ShowAtPosition(Vector2 screenTapPosition)
    {
        if (!_image.enabled)
            _image.enabled = true;
        SetPosition(screenTapPosition);
    }

    private void SetPosition(Vector2 screenTapPosition)
    {
        _rectTransform.anchoredPosition = CanvasPositioningHelper.ScreenPointToRect(screenTapPosition, _canvas);
    }

    public void Hide()
    {
        if (_image.enabled)
            _image.enabled = false;
    }
}
    