using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Vector3 _cameraOffset;

    private void Start()
    {
        _cameraOffset = _player.transform.position - transform.position;
    }

    void Update()
    {
        transform.position = _player.transform.position - _cameraOffset;
    }
}
