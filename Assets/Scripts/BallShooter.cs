using UnityEngine;
using System;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private BallCreator _ballCreator;
    [SerializeField] private BallTrajectoryRenderer _ballTrajectoryRenderer;
    [SerializeField] private float _shootForce = 10f;

    private Ball _ball;
    private Vector2 _dragStart;
    private Vector2 _dragDirection;
    private Vector2 _ballPosition;
    private bool _isDragging = false;

    public event Action OnShoot;

    private Vector2 DragForce => _dragDirection * _shootForce;

    private void Start()
    {
        _ball = _ballCreator.CreateBulletBall(transform.position);
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
        _ballTrajectoryRenderer.DrawTrajectory(_dragDirection, _ballPosition);
    }

    private void ClearBallTrajectory()
    {
        _ballTrajectoryRenderer.Clear();
    }

    private void ShootBall()
    {
        _ball.Shoot(DragForce);
        //_ball = _ballCreator.CreateBulletBall(transform.position);

        OnShoot?.Invoke();
    }
}