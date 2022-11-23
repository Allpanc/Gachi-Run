using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    #region("Fields")
    [Header("Speed parameters")]
    [SerializeField] private float _initialRunningSpeed = 12;
    [SerializeField] private float _rotationSpeed = 20;
    [SerializeField] private float _verticalRunningSpeed = 10;
    [SerializeField] private float _fallingSpeed = -10;
    [Header("Other parameters")]
    [SerializeField] private float _rotationAngle = 55;

    private float _currentRunningSpeed = 12;
    private Gyroscope _gyroscope;
    private List<PlayerBaseState> _states = new List<PlayerBaseState>();
    private float rotY = 0;

    public Rigidbody _rb { get; private set; }
  
    public PlayerBaseState _currentState { get; private set; }

    [Header("States")]
    public RunningState _runningState;
    public JumpingState _jumpingState;
    public WallRunningState _wallRunningState;
    public FallingState _fallingState;
    public DamagedState _damagedState;
    #endregion

    void Start()
    {
#if !UNITY_EDITOR
        _gyroscope = Input.gyro;
        _gyroscope.enabled = true;
#endif

        _rb = GetComponent<Rigidbody>();
        _currentRunningSpeed = _initialRunningSpeed;

        _states.Add(_runningState);
        _states.Add(_jumpingState);
        _states.Add(_wallRunningState);
        _states.Add(_fallingState);
        _states.Add(_damagedState);

        InitializeStates();

        SetState(_runningState);
    }

    void Update()
    {
        _currentState.Update();
    }

    public void SetState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.Enter();
    }

    public void ResetOtherStates()
    {
        foreach (PlayerBaseState state in _states)
            if (_currentState != state)
                state.Reset();
    }

    public void InitializeStates()
    {
        foreach (PlayerBaseState state in _states)
            state.Initialize(this);
    }

    public void RotateToGyro()
    {
#if !UNITY_EDITOR
        if (_gyroscope != null && _currentState != _wallRunningState)
                if (_gyroscope.gravity.x * _rotationAngle < _rotationAngle)
                    _rb.rotation = Quaternion.Euler(0, _gyroscope.gravity.x * _rotationAngle, 0);
            else
                _rb.rotation = Quaternion.Euler(Vector3.zero);
#else
        if (_currentState != _wallRunningState)
        {
            if (Input.GetKey(KeyCode.A))
                rotY -= Time.deltaTime * 3;
            else if (Input.GetKey(KeyCode.D))
                rotY += Time.deltaTime * 3;
                
            if (Mathf.Abs(rotY * _currentRunningSpeed) < _rotationAngle)
                _rb.rotation = Quaternion.Euler(0, rotY * _currentRunningSpeed, 0);
            else
                rotY = rotY>0?(_rotationAngle/_currentRunningSpeed): (-_rotationAngle / _currentRunningSpeed);
        }
        else
        {
            rotY = 0;
            _rb.rotation = Quaternion.Euler(Vector3.zero);
        }
#endif
    }


    #region("Collision handling")

    private void OnCollisionStay(Collision collision)
    {
        _currentState.Update(collision);
        //Debug.Log("Contact count " + collision.contactCount);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Contact count " + collision.contactCount);
        collision = null;
        _currentState.Update(collision);
    }
    #endregion

    #region("Getters and setters")

    public float GetSpeed() => _currentRunningSpeed;
    public float SetSpeed(float speed) => _currentRunningSpeed = speed;
    public float GetInitialSpeed() => _initialRunningSpeed;
    public float GetVerticalSpeed() => _verticalRunningSpeed;
    public float GetRotationSpeed() => _rotationSpeed;
    public float GetFallingSpeed() => _fallingSpeed;
    public float GetRotationAngle() => _rotationAngle;
    public Vector3 GetRotation() => _rb.rotation.eulerAngles;
    public Vector3 GetGravity() => _gyroscope.gravity;

    public void ChangeSpeed(float deltaSpeed)
    {
        _currentRunningSpeed += deltaSpeed;
    }

    public void ChangeRotationSpeed(float deltaRotationSpeed)
    {
        _rotationAngle += deltaRotationSpeed;
    }
    #endregion
}
