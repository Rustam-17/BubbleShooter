using UnityEngine;

public class BallTrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private Color _trajectoryLineColor;
    [SerializeField] private Color _spreadLineColor;

    private Vector2 _trajectoryEnd;

    public void DrawTrajectory(Vector2 dragDirection, Vector2 _trajectoryStart)
    {
        _trajectoryEnd = _trajectoryStart + dragDirection;

        _trajectoryLine.positionCount = 2;
        _trajectoryLine.SetPosition(0, _trajectoryStart);
        _trajectoryLine.SetPosition(1, _trajectoryEnd);
    }

    public void Clear()
    {
        _trajectoryLine.positionCount = 0;
    }
}
