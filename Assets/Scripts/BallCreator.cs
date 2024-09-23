using UnityEngine;

public enum BallColor
{
    red,
    green,
    cyan,
    yellow,
    white
}

public class BallCreator : MonoBehaviour
{
    private const char RED_COLOR_SYMBOL = 'R';
    private const char GREEN_COLOR_SYMBOL = 'G';
    private const char CYAN_COLOR_SYMBOL = 'C';
    private const char YELLOW_COLOR_SYMBOL = 'Y';

    [SerializeField] private GameObject _ballPrefab;

    private GameObject _ballObject;
    private Ball _ball;
    private SpriteRenderer _ballSpriteRenderer;
    private SpringJoint2D _ballJoint;
    private Rigidbody2D _ballRigidbody;

    public Ball CreateGameGridBall(Vector2 position, char colorSymbol, Transform gridTransform)
    {
        _ballObject = Instantiate(_ballPrefab, position, Quaternion.identity, gridTransform);

        _ball = _ballObject.GetComponent<Ball>();
        _ball.SetColor(GetBallColorFromMatrix(colorSymbol));

        _ballSpriteRenderer = _ballObject.GetComponent<SpriteRenderer>();
        _ballSpriteRenderer.color = GetColorFromMatrix(_ball.Color);

        _ballJoint = _ballObject.GetComponent<SpringJoint2D>();
        _ballJoint.connectedAnchor = position;

        return _ball;
    }

    public Ball CreateBulletBall(Vector2 position)
    {
        _ballObject = Instantiate(_ballPrefab, position, Quaternion.identity);

        _ball = _ballObject.GetComponent<Ball>();
        _ball.SetColor(GetRandomBallColor());

        _ballSpriteRenderer = _ballObject.GetComponent<SpriteRenderer>();
        _ballSpriteRenderer.color = GetColorFromMatrix(_ball.Color);

        _ballRigidbody = _ballObject.GetComponent<Rigidbody2D>();
        _ballRigidbody.bodyType = RigidbodyType2D.Static;
        _ballRigidbody.mass = 1.0f;

        _ballJoint = _ballObject.GetComponent<SpringJoint2D>();
        _ballJoint.enabled = false;

        return _ball;
    }

    public float GetBallSize()
    {
        float ballSize = _ballPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        return ballSize;
    }

    private BallColor GetBallColorFromMatrix(char symbol)
    {
        switch (symbol)
        {
            case RED_COLOR_SYMBOL: return BallColor.red;
            case GREEN_COLOR_SYMBOL: return BallColor.green;
            case CYAN_COLOR_SYMBOL: return BallColor.cyan;
            case YELLOW_COLOR_SYMBOL: return BallColor.yellow;
            default: return BallColor.white;
        }
    }

    private Color GetColorFromMatrix(BallColor ballColor)
    {
        switch (ballColor)
        {
            case BallColor.red: return Color.red;
            case BallColor.green: return Color.green;
            case BallColor.cyan: return Color.cyan;
            case BallColor.yellow: return Color.yellow;
            default: return Color.white;
        }
    }

    private BallColor GetRandomBallColor()
    {
        BallColor[] colors = (BallColor[])System.Enum.GetValues(typeof(BallColor));

        int randomIndex = Random.Range(0, colors.Length - 1);

        return colors[randomIndex];
    }
}
