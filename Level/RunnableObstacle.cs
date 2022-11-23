using UnityEngine;

public class RunnableObstacle : Obstacle
{

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != _player.gameObject)
            return;

        else if (CollisionClassifier.IsCollisionGround(collision))
        {
            if (_player._currentState != _player._runningState && _player._currentState != _player._damagedState)
                _player.SetState(_player._runningState);
            Debug.Log("Found Ground, back to running");
        }
        else
            Debug.Log("Obstacle but not runnable");
    }
}
