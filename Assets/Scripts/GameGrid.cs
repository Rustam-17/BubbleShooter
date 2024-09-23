using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid: MonoBehaviour
{
    private int _width;
    private int _height;

    private Ball[,] _balls;

    public void SetSize(int width, int height)
    {
        _width = width;
        _height = height;

        _balls = new Ball[_width, _height];
    }

    public void Add(Ball ball, int ballPositionX, int ballPositionY)
    {
        _balls[ballPositionX, ballPositionY] = ball;
    }

    public Ball GetBall(int ballPositionX, int ballPositionY)
    {
        return _balls[ballPositionX, ballPositionY];
    }

    public void Remove(List<Vector2Int> ballPositions)
    {
        foreach (Vector2Int ballposition in ballPositions)
        {
            _balls[ballposition.x, ballposition.y] = null;
        }
    }
}
