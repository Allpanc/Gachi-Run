using UnityEngine;

public abstract class PlayerBaseState : ScriptableObject
{
    protected Rigidbody _rigidbody;
    protected Animator _animator;
    protected Player _player;
    protected PlayerInputController _playerInput;
    protected Collision _currentCollision;
    protected Vector3 _currentCollisionNormal;
    protected Vector3 _currentCollisionTangent;

    public virtual void Initialize(Player player)
    {
        _player = player;
        _rigidbody = player.GetComponent<Rigidbody>();
        _animator = player.GetComponent<Animator>();
        _playerInput = FindObjectOfType<PlayerInputController>();
    }

    public virtual void Enter() 
    {
        _player.ResetOtherStates();
    }

    public virtual void Update() 
    {
        _player.RotateToGyro();
        CheckForTransitions();
    }

    public virtual void Update(Collision collision)
    {
        _player.RotateToGyro();

        if (collision != null && collision.contactCount>0)
        {
            _currentCollision = collision;
            _currentCollisionNormal = _currentCollision.contacts[0].normal.normalized;
            _currentCollisionTangent = Vector3.Cross(_currentCollisionNormal, Vector3.left).normalized;
            //Debug.DrawRay(_currentCollision.contacts[0].point, _currentCollisionNormal, Color.red, 10f);
            //Debug.DrawRay(_currentCollision.contacts[0].point, Vector3.up * 2, Color.green, 10f);
        }
    }
    
    public virtual void CheckForTransitions()
    {
        if ( _currentCollision == null)
        {
            if (_rigidbody.velocity.y < _player.GetFallingSpeed() && _player._currentState != _player._fallingState)
                _player.SetState(_player._fallingState);
        }
        else if (CollisionClassifier.IsCollisionBarrier(_currentCollision))
                _player.SetState(_player._damagedState);
    }

    public abstract void Reset();
}
