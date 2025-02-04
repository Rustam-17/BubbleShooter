using UnityEngine;

public class TrajectoryCalculator
{
    private const float GRAVITY = 9.8f;

    private GameGrid _gameGrid;
    private Vector2[] _trajectoryPoints;
    private int _numPoints;
    private float _timeStep;
    private float _leftWallX;
    private float _rightWallX;
    private float _ceilingY;
    private float _reflectionEnergyLoss = 0.7f;

    public TrajectoryCalculator(GameGrid gameGrid, int numPoints, float timeStep, float ballRadius)
    {
        _gameGrid = gameGrid;
        _numPoints = numPoints;
        _timeStep = timeStep;
        _trajectoryPoints = new Vector2[numPoints];

        CalculateScreenBounds(ballRadius);
    }

    public Vector2[] GetTrajectoryPoints(Vector2 startPosition, Vector2 initialVelocity)
    {
        CalculateTrajectory(startPosition, initialVelocity);

        Vector2[] trajectoryPoints = new Vector2[_numPoints];

        for (int i = 0; i < _numPoints; i++)
        {
            trajectoryPoints[i] = _trajectoryPoints[i];
        }

        return trajectoryPoints;
    }

    private void CalculateScreenBounds(float ballRadius)
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        _leftWallX = bottomLeft.x + ballRadius;
        _rightWallX = topRight.x - ballRadius;
        _ceilingY = topRight.y - ballRadius;
    }

    private void CalculateTrajectory(Vector2 startPosition, Vector2 initialVelocity)
    {        
        Vector2 currentPosition = startPosition;
        Vector2 currentVelocity = initialVelocity;
        bool collisionDetected = false;

        for (int i = 0; i < _numPoints; i++)
        {
            if (collisionDetected)
            {
                _trajectoryPoints[i] = currentPosition;
                continue;
            }

            Vector2 newPosition = currentPosition + currentVelocity * _timeStep;
            Vector2 newVelocity = currentVelocity;

            newVelocity.y -= GRAVITY * _timeStep;

            if (newPosition.x <= _leftWallX)
            {
                newPosition.x = _leftWallX;
                newVelocity.x = -newVelocity.x * _reflectionEnergyLoss;
            }
            else if (newPosition.x >= _rightWallX)
            {
                newPosition.x = _rightWallX;
                newVelocity.x = -newVelocity.x * _reflectionEnergyLoss;
            }

            if (newPosition.y >= _ceilingY)
            {
                newPosition.y = _ceilingY;
                newVelocity.y = -newVelocity.y * _reflectionEnergyLoss;
            }

            _trajectoryPoints[i] = newPosition;

            Vector2Int? collisionCell = _gameGrid.CheckCollision(newPosition);

            if (collisionCell.HasValue)
            {
                collisionDetected = true;
                currentPosition = _gameGrid.GetCellWorldPosition(collisionCell.Value);
            }

            currentPosition = newPosition;
            currentVelocity = newVelocity;
        }
    }
}
