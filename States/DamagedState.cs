using UnityEngine;

[CreateAssetMenu(fileName = "Damaged State", menuName = "Player States/Damaged State")]
public class DamagedState : PlayerBaseState
{
    
    [SerializeField] float _damagedMaxTime = 0.5f;
    [SerializeField] float _hitForce = 8;
    [SerializeField] float _damagedSpeed = 2;
    private float _damagedTimer;
    private Vector3 _hitDirection;

    public override void Enter()
    {
        base.Enter();
        _damagedTimer = _damagedMaxTime;
        _hitDirection = -_rigidbody.velocity.normalized;

        if (_hitDirection.y > 0)
            _hitDirection = new Vector3(_hitDirection.x, -_hitDirection.y, _hitDirection.z);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(_hitDirection * _hitForce, ForceMode.Impulse);
        Debug.Log("Damaged state");
    }

    public override void Update()
    {
        if (_damagedTimer >= 0)
            _damagedTimer -= Time.deltaTime;
        else
        {
            _player.SetSpeed(_damagedSpeed);
            _player.SetState(_player._runningState);
        }    
    }

    public override void Reset()
    {
        _damagedTimer = _damagedMaxTime;
    }
}
