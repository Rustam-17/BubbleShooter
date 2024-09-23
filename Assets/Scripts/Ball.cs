using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private BallColor _color;

    public BallColor Color => _color;

    public void SetColor(BallColor color)
    {
        _color = color;
    }

    public void Shoot(Vector2 force)
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.velocity = force;
    }
}
