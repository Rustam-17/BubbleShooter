using UnityEngine;

public class BallTrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _trajectoryLine;

    private Vector2 _trajectoryEnd;

    public void DrawTrajectory(Vector2 dragDirection, Vector2 _trajectoryStart)
    {
        _trajectoryEnd = _trajectoryStart + dragDirection;

        _trajectoryLine.positionCount = 2;
        _trajectoryLine.SetPosition(0, _trajectoryStart);
        _trajectoryLine.SetPosition(1, _trajectoryEnd);
    }

    public void DrawTrajectory(Vector2[] points)
    {
        _trajectoryLine.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            _trajectoryLine.SetPosition(i, points[i]);
        }
    }

    public void Clear()
    {
        _trajectoryLine.positionCount = 0;
    }
}
