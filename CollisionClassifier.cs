using UnityEngine;

public class CollisionClassifier : MonoBehaviour
{
    private static Vector3 _collisionNormal;
    private static float _maxGroundAngle = 45;
    private static float _maxBarrierAngle = 45;

    private enum CollisionType
    {
        Ground,
        Wall,
        Slope,
        Barrier
    }

    public static bool IsCollisionWall(Collision collision)
    {
        if (IsCollisionUnknown(collision))
            return false;
        _collisionNormal = collision.contacts[0].normal.normalized;
        return  _collisionNormal == Vector3.right || _collisionNormal == Vector3.left;
    }

    public static bool IsCollisionGround(Collision collision)
    {
        if (IsCollisionUnknown(collision))
            return false;
        _collisionNormal = collision.contacts[0].normal.normalized;
        float angle = Mathf.Abs(Vector3.Angle(_collisionNormal, Vector3.up) - 180);
        //Debug.Log(angle);
        return angle <= _maxGroundAngle;
    }

    public static bool IsCollisionBarrier(Collision collision)
    {
        if (IsCollisionUnknown(collision))
            return false;
        _collisionNormal = collision.contacts[0].normal.normalized;
        float angle = Mathf.Abs(Vector3.Angle(_collisionNormal, Vector3.back));
        return angle <= _maxBarrierAngle;
        //Debug.Log(angle);
        //Debug.DrawRay(collision.GetContact(0).point, _collisionNormal, Color.red, 10f);
        //Debug.DrawRay(collision.GetContact(0).point, Vector3.back * 2, Color.green, 10f);

    }

    public static bool IsCollisionUnknown(Collision collision)
    {
        return collision == null || collision.contactCount == 0;
    }
}
