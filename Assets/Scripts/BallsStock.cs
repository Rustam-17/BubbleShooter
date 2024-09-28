using UnityEngine;

public class BallsStock : MonoBehaviour
{
    [SerializeField] private BallCreator _ballCreator;
    [SerializeField] private BallsCount _ballsCount;
    [SerializeField] private int _totalBallsCount;

    private Ball[] _balls;
    private Ball _nextBall;

    private int LastBallIndex => _ballsCount.Volume - 1;

    private void Start()
    {
        _ballsCount.Add(_totalBallsCount);
        _balls = new Ball[_ballsCount.Volume];

        FillUp();
    }

    public bool TryGetBall(out Ball nextBall)
    {
        nextBall = _nextBall;

        if (nextBall == false)
        {
            return false;
        }

        SetNextBall();
        
        return true;
    }

    private void FillUp()
    {
        Ball ball;

        for (int i = 0; i < _balls.Length; i++)
        {
            ball = _ballCreator.CreateRandomBulletBall(transform.position, transform);

            _balls[i] = ball;
        }

        _nextBall = _balls[LastBallIndex];
    }

    private void SetNextBall()
    {
        if (_ballsCount.TrySubtract(1) && _ballsCount.Volume > 0)
        {
            _nextBall = _balls[LastBallIndex];
        }
        else
        {
            _nextBall = null;
        }
    }
}
