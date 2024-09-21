using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    private int _height;
    private int _width;

    private Ball[,] _balls;

    public GameGrid(int height, int width)
    {
        _height = height;
        _width = width;

        _balls = new Ball[_height, _width];
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
