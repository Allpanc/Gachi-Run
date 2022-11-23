using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [SerializeField] private TapCursor _tapCursor;
    [SerializeField] private Player _player;

    public bool _isWallRunningEnabled;

    private void Start()
    {
        if (_player == null)
            _player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _tapCursor.ShowAtPosition(eventData.pressPosition);
        _isWallRunningEnabled = true;
        _player.SetState(_player._jumpingState);
        Debug.LogWarning("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _tapCursor.Hide();
        _isWallRunningEnabled = false;
        Debug.LogWarning("Pointer Up");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (PointerIsInsideInputArea(eventData))
        {
            _tapCursor.ShowAtPosition(eventData.pointerCurrentRaycast.screenPosition);
        }
        else
            _tapCursor.Hide();
        //Debug.LogWarning("Drag");
    }

    private bool PointerIsInsideInputArea(PointerEventData eventData)
    {
        return eventData.pointerCurrentRaycast.gameObject == gameObject;
    }
}
