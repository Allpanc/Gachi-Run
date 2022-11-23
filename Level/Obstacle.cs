using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public virtual void OnCollisionEnter(Collision collision) { }
}
