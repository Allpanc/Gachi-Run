using UnityEngine;

[CreateAssetMenu(fileName = "Jumping State", menuName = "Player States/Jumping State")]
public class JumpingState : PlayerBaseState
{
    [SerializeField] int _jumpCount = 0;
    [SerializeField] int _wallJumpCount = 0;
    [SerializeField] private float _jumpForce = 7;
    [SerializeField] float _doubleJumpForce = 1;
    [SerializeField] float _wallJumpForce = 1;   
    [SerializeField] float _wallJumpMaxTime = 0.4f;   
    [SerializeField] bool _hasWallJumped;
    private float _wallJumpTimer;

    private enum JumpVariation
    {
        Jump,
        DoubleJump,
        WallJump
    }

    public override void Initialize(Player player)
    {
        base.Initialize(player);
    }

    public override void Enter()
    {
        base.Enter();
        _wallJumpTimer = _wallJumpMaxTime;
        //Debug.LogWarning(_playerInput._isWallRunningEnabled);
        if (CollisionClassifier.IsCollisionWall(_currentCollision) && !_playerInput._isWallRunningEnabled)
            WallJump();
        else if (_jumpCount == 0 && !CollisionClassifier.IsCollisionWall(_currentCollision))
            Jump();
        else if (_jumpCount == 1 && _rigidbody.velocity.y > _player.GetFallingSpeed() && !CollisionClassifier.IsCollisionWall(_currentCollision))
            DoubleJump();

        //Debug.Log("Jumping State: " + _currentCollision.gameObject.name);
    }

    public override void Update()
    {
        base.Update();      
        if (_hasWallJumped)
        {
            _wallJumpTimer -= Time.deltaTime;
            if (_wallJumpTimer <= 0)
            {
                _hasWallJumped = false;
                _wallJumpTimer = _wallJumpMaxTime;
            }
        }
        else
        {
            if (_player._rb.velocity.y >= _player.GetVerticalSpeed())
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _player.GetVerticalSpeed(), _rigidbody.velocity.y);
            else
                _rigidbody.velocity = new Vector3(_player.transform.forward.normalized.x * _player.GetRotationSpeed(), _rigidbody.velocity.y, _player.transform.forward.normalized.z * _player.GetSpeed());
        }
    }

    public override void Update(Collision collision)
    {
        base.Update(collision);
        CheckForTransitions();
    }

    public override void CheckForTransitions()
    {
        if (CollisionClassifier.IsCollisionWall(_currentCollision) && _playerInput._isWallRunningEnabled && _jumpCount > 0)
        {
           // Debug.Log("Found " + _currentCollision.gameObject.tag);
            _player.SetState(_player._wallRunningState);
        }
        base.CheckForTransitions();
    }

    public override void Reset()
    {
        _animator.SetBool("isJumping", false);
        _animator.SetBool("isDoubleJumping", false);
        _jumpCount = 0;
        _hasWallJumped = false;
        _wallJumpTimer = _wallJumpMaxTime;
        //Debug.Log("Reset jump");
    }

    public void Jump()
    {
        PerformJumpVariation(Vector3.up, _jumpForce, JumpVariation.Jump);
        Debug.Log("Jump, jump count = " + (_jumpCount));
    }

    public void DoubleJump()
    {
        PerformJumpVariation(Vector3.up, _doubleJumpForce, JumpVariation.DoubleJump);
        Debug.Log("Double jump, jump count = " + (_jumpCount));
    }

    public void WallJump()
    {
        Vector3 wallJumpDirection = new Vector3(_currentCollisionNormal.x, 1, 0);
        PerformJumpVariation(wallJumpDirection, _wallJumpForce, JumpVariation.WallJump);
        _hasWallJumped = true;
        Debug.Log("Wall jump, jump count = " + _jumpCount);
    }

    private void PerformJumpVariation(Vector3 jumpDirection, float jumpForce, JumpVariation jumpVariation)
    {     
        string jumpAnimationBoolName;
        switch (jumpVariation)
        {
            case JumpVariation.Jump:
            case JumpVariation.WallJump:
            default:
                jumpAnimationBoolName = "isJumping";
                break;
            case JumpVariation.DoubleJump:
                jumpAnimationBoolName = "isDoubleJumping";
                break;
        }
        _rigidbody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        _animator.SetBool(jumpAnimationBoolName, true);
        _jumpCount++;
        //_playerInput._isWallRunningEnabled = false;
    }
}
