using UnityEngine;
using TMPro;

public class ShowStats : MonoBehaviour
{
    public Player _player;
    public TMP_Text _rotation;
    public TMP_Text _rotAngle;
    public TMP_Text _speed;
    public TMP_Text _gravity;
    public TMP_Text _fps;

    private float deltaTime = 0;

    void Update()
    {
        _rotation.text = "Rotation = " + _player.GetRotation().ToString();
        _rotAngle.text = "Max rot angle = " + _player.GetRotationAngle().ToString(); 
        _speed.text = "Speed = " + _player.GetSpeed().ToString();
        _gravity.text = "Grav = " + _player.GetGravity().ToString();

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _fps.text = "fps = " + Mathf.Ceil(fps).ToString();
    }
}
