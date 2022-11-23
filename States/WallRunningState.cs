using UnityEngine;

[CreateAssetMenu(fileName ="Wall Running State", menuName = "Player States/Wall Running State")]
public class WallRunningState : PlayerBaseState
{
    [SerializeField] float _wallRunTime = 5f;

    public override void Initialize(Player player)
    {
        base.Initialize(player);
    }

    public override void Enter()
    {
        base.Enter();
        _animator.SetBool("isWallRunning", true);
        //if (_currentCollision != null)
        //    Debug.Log("Wall Running State = " + _currentCollision.gameObject.tag);
    }

    public override void Update()
    {
        _wallRunTime -= Time.deltaTime;

        if (!_playerInput._isWallRunningEnabled || _wallRunTime < 0)
            _player.SetState(_player._jumpingState);
        else
        {
            Vector3 velocity = new Vector3(0, 0, _player.transform.forward.normalized.z * _player.GetSpeed());
            _rigidbody.velocity = velocity;
        }
    }

    public override void CheckForTransitions()
    {
        base.CheckForTransitions();
    }

    public override void Reset()
    {
        _animator.SetBool("isWallRunning", false);
        _wallRunTime = 5;
    }
}
