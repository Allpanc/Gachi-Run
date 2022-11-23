using UnityEngine;

[CreateAssetMenu(fileName = "Running State", menuName = "Player States/Running State")]
public class RunningState : PlayerBaseState
{
    [SerializeField] Vector3 _normalGravity = new Vector3(0, -9.81f, 0);
    [SerializeField] Vector3 _increasedGravity = new Vector3(0, -35f, 0);
    [SerializeField] float _acceleration = 2;

    public override void Initialize(Player player)
    {
        base.Initialize(player);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Running State");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Update(Collision collision)
    {
        base.Update(collision);
        if (_player.GetSpeed() < _player.GetInitialSpeed())
            _player.SetSpeed(_player.GetSpeed() + Time.deltaTime * _acceleration);

        if (Input.GetKey(KeyCode.Space))
            _player.SetSpeed(_player.GetSpeed() + Time.deltaTime * _acceleration);

        if (_currentCollision == null)
        {
            Physics.gravity = _increasedGravity;
            _rigidbody.velocity = new Vector3(_player.transform.forward.normalized.x * _player.GetRotationSpeed(), _rigidbody.velocity.y, _player.transform.forward.normalized.z * _player.GetSpeed());
            //Debug.LogWarning("Extra gravity");
        }
        else
        {
            Physics.gravity = _normalGravity;
            if (CollisionClassifier.IsCollisionWall(_currentCollision))
                _rigidbody.velocity = new Vector3(_player.transform.forward.normalized.x * _player.GetRotationSpeed(), _rigidbody.velocity.y, _player.transform.forward.normalized.z * _player.GetSpeed());
            else
                _rigidbody.velocity = new Vector3(_player.transform.forward.normalized.x * _player.GetRotationSpeed(), _currentCollisionTangent.y * _player.GetVerticalSpeed(), _player.transform.forward.normalized.z * _player.GetSpeed());
            //Debug.LogWarning(_currentCollision.contacts[0].normal);
        }
        //Debug.DrawRay(_player.transform.position, Vector3.forward, Color.red, 10);
        //Debug.DrawRay(_player.transform.position, _currentCollisionTangent, Color.red, 10);
    }

    public override void Reset() 
    {
        Physics.gravity = _normalGravity;
    }
}
