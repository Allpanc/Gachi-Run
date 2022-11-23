using UnityEngine;

[CreateAssetMenu(fileName = "Falling State", menuName = "Player States/Falling State")]
public class FallingState : PlayerBaseState
{
    
    public override void Initialize(Player player)
    {
        base.Initialize(player);
    }

    public override void Enter()
    {
        base.Enter();
        _animator.SetBool("isFalling", true);
        Debug.Log("Falling State");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Update(Collision collision)
    {
        base.Update(collision);
    }

    public override void CheckForTransitions()
    {
        base.CheckForTransitions();
        Debug.Log("Finding transitions");
        if (_currentCollision == null)
            return;

        if (CollisionClassifier.IsCollisionWall(_currentCollision) && _playerInput._isWallRunningEnabled)
        {
            Debug.Log("Found " + _currentCollision.gameObject.tag);
            _player.SetState(_player._wallRunningState);
        }
        else if (CollisionClassifier.IsCollisionBarrier(_currentCollision)/* && _rigidbody.velocity.y <= 0*/)
        {
            Debug.Log("Found " + _currentCollision.gameObject.tag);
            _player.SetState(_player._damagedState);
        }
        else
            _player.SetState(_player._runningState);
    }

    public override void Reset()
    {
        _animator.SetBool("isFalling", false);
    }
}
