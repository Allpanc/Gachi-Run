using UnityEngine;

public class BarrierObstacle : Obstacle
{
    public override void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject == _player && !CollisionRecognitionHelper.IsCollisionWall(collision))
        //{
        //    if (_player._currentState != _player._runningState)
        //        _player.SetState(_player._runningState);
        //    Debug.Log("Back to running");
        //}
    }
}
