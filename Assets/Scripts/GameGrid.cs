using System.Collections.Generic;
using UnityEngine;

public class GameGrid: MonoBehaviour
{
    private int _width;
    private int _height;
    private float _hexHeightFactor;
    private float _ballSize;
    private float _spacing;

    private Ball[,] _balls;

    public void SetParameters(int width, int height, float hexHeightFactor, float ballSize, float spacing)
    {
        _width = width;
        _height = height;
        _hexHeightFactor = hexHeightFactor;
        _ballSize = ballSize;
        _spacing = spacing;

        _balls = new Ball[_width, _height];
    }

    public void Add(Ball ball, int ballPositionX, int ballPositionY)
    {
        _balls[ballPositionX, ballPositionY] = ball;
    }

    public Vector2Int? CheckCollision(Vector2 position)
    {
        Vector2Int nearestCell = GetGridCellFromWorldPosition(position);

        if (nearestCell.x < 0 || nearestCell.x >= _width || nearestCell.y < 0 || nearestCell.y >= _height)
            return null;

        if (_balls[nearestCell.x, nearestCell.y] == null)
            return nearestCell;

        return null;
    }

    public Vector2 GetCellWorldPosition(Vector2Int cellIndex)
    {
        float offsetX = (cellIndex.y % 2 == 0) ? 0 : _ballSize * 0.5f * _spacing;
        float x = cellIndex.x * _ballSize * _spacing + offsetX;
        float y = -cellIndex.y * _ballSize * _spacing * _hexHeightFactor;

        return new Vector2(x, y);
    }
    
    private Vector2Int GetGridCellFromWorldPosition(Vector2 worldPosition)
    {
        int y = Mathf.RoundToInt(-worldPosition.y / (_ballSize * _spacing * _hexHeightFactor));
        float offsetX = (y % 2 == 0) ? 0 : _ballSize * 0.5f * _spacing;
        int x = Mathf.RoundToInt((worldPosition.x - offsetX) / (_ballSize * _spacing));

        return new Vector2Int(x, y);
    }

    public Ball GetBall(int ballPositionX, int ballPositionY)
    {
        return _balls[ballPositionX, ballPositionY];
    }

    public void Remove(List<Vector2Int> ballPositions)
    {
        foreach (Vector2Int ballPosition in ballPositions)
        {
            _balls[ballPosition.x, ballPosition.y] = null;
        }
    }
}
