using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpringJoint2D _springJoint;
    [SerializeField] private BallMovement _movement;

    private BallColor _color;

    public BallColor Color => _color;

    public void SetColor(BallColor color)
    {
        _color = color;
    }

    public void Shoot(Vector2[] points, float timeStep)
    {
        _movement.Move(points, timeStep);
    }

    public void ChargeBallShooter(Transform ballSooterTransform, float chargeDuration)
    {
        transform.SetParent(ballSooterTransform);

        _movement.Move(ballSooterTransform.position, chargeDuration);
    }
}
