using UnityEngine;
using System;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private BallTrajectoryRenderer _ballTrajectoryRenderer;
    [SerializeField] private BallsStock _ballsStock;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _chargeDuration;
    [SerializeField] private int _trajectoryPointsCount;
    [SerializeField] private float _trajectoryTimeStep;

    private TrajectoryCalculator _trajectoryCalculator;
    private Ball _ball;
    private Vector2 _dragStart;
    private Vector2 _dragDirection;
    private Vector2 _ballPosition;
    private bool _isDragging;
    private float _ballRadius;

    public event Action OnShoot;

    private Vector2 DragForce => _dragDirection * _shootSpeed;

    private void Start()
    {
        Charge();

        _ballRadius = GetBallRadius();
        _trajectoryCalculator = new TrajectoryCalculator(_trajectoryPointsCount, _trajectoryTimeStep, _ballRadius);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsBallClicked())
        {
            _isDragging = true;

            _dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _dragDirection = _dragStart - currentMousePosition;
            _ballPosition = _ball.transform.position;
            
            DrawBallTrajectory();
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _isDragging = false;

            ShootBall();
            ClearBallTrajectory();
        }
    }

    private bool IsBallClicked()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        return hit.collider != null && hit.collider.gameObject == _ball.gameObject;
    }

    private void DrawBallTrajectory()
    {
        _ballTrajectoryRenderer.DrawTrajectory(_trajectoryCalculator.GetTrajectoryPoints(_ballPosition, DragForce));
    }

    private void ClearBallTrajectory()
    {
        _ballTrajectoryRenderer.Clear();
    }

    private void ShootBall()
    {
        _ball.Shoot(_trajectoryCalculator.GetTrajectoryPoints(_ballPosition, DragForce), _trajectoryTimeStep);
        OnShoot?.Invoke();

        Charge();
    }

    private void Charge()
    {
        if (_ballsStock.TryGetBall(out Ball ball) == false)
        {
            return;
        }

        _ball = ball;
        _ball.ChargeBallShooter(transform, _chargeDuration);
    }

    private float GetBallRadius()
    {
        return _ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }
}