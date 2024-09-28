using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private float _speed;
    private float _moveProgress;
    private bool _isMoving;

    void Update()
    {        
        if (_isMoving)
        {
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _moveProgress);

            _moveProgress += Time.deltaTime * _speed;

            if (_moveProgress >= 1f)
            {
                transform.position = _targetPosition;
                _isMoving = false;
            }
        }
    }

    public void Move(Vector2 targetPosition, float chargeSpeed)
    {
        _startPosition = transform.position;
        _targetPosition = targetPosition;
        _speed = chargeSpeed;
        _moveProgress = 0f;
        _isMoving = true;
    }
}