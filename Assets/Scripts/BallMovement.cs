using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Vector2[] _trajectoryPoints;
    private Vector2 _startPoint;
    private int _currentPointIndex;
    private float _timeStep;
    private float _elapsedTime;
    private float _elapsedTimeFraction;
    private bool _isMoving;

    void FixedUpdate()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.fixedDeltaTime;
            _elapsedTimeFraction = _elapsedTime / _timeStep;

            transform.position = Vector2.Lerp(_startPoint, _trajectoryPoints[_currentPointIndex], _elapsedTimeFraction);

            if (_elapsedTimeFraction >= 1)            
            {
                _currentPointIndex++;
                _elapsedTime = 0;
                _startPoint = transform.position;
            }

            if (_currentPointIndex >= _trajectoryPoints.Length)
            {
                _isMoving = false;
            }
        }
    }

    public void Move(Vector2[] points, float timeStep)
    {
        _trajectoryPoints = points;

        _currentPointIndex = 0;
        _isMoving = true;

        _timeStep = timeStep;
        _elapsedTime = 0;

        _startPoint = transform.position;
    }

    public void Move(Vector2 targetPoint, float duration)
    {
        _trajectoryPoints = new Vector2[] { targetPoint };

        _currentPointIndex = 0;
        _isMoving = true;

        _timeStep = duration;
        _elapsedTime = 0;

        _startPoint = transform.position;
    }
}