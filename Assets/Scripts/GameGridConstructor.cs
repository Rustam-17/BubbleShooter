using UnityEngine;

public class GameGridConstructor : MonoBehaviour
{
    [SerializeField] private string _matrixFileName;
    [SerializeField] private int _levelNumber;
    [SerializeField] private char _emptyCellSymbol;
    [SerializeField] private GameGrid _gameGrid;
    [SerializeField] private BallCreator _ballCreator;

    private Ball _ball;    
    private Transform _gameGridTransform;
    private TextAsset _gridMatrix;
    private string[] _gridMatrixLines;
    private float _ballSize;
    private float _spacing;
    private float _hexHeightFactor;
    private float _absoluteGridWidth;
    private float _gameGridPositionY;

    private string MatrixFileName => _matrixFileName + _levelNumber;
    private string MatrixFilePath => $"LevelGridMatrices/{MatrixFileName}";

    private void Start()
    {
        _gameGridTransform = _gameGrid.transform;
        _gameGridPositionY = _gameGridTransform.position.y;
        _ballSize = _ballCreator.GetBallSize();
        _spacing = 1.1f;
        _hexHeightFactor = 0.866f;

        CreateGridFromMatrix();
    }

    private void CreateGridFromMatrix()
    {
        if (TryLoadGridMatrix() == false)
        {
            return;
        }

        _gridMatrixLines = _gridMatrix.text.Split('\n');

        CreateGameGrid();
    }

    private bool TryLoadGridMatrix()
    {
        _gridMatrix = Resources.Load<TextAsset>(MatrixFilePath);

        if (_gridMatrix == null)
        {
            Debug.LogError($"Matrix file {MatrixFileName} not found!");

            return false;
        }

        return true;
    }

    private void CreateGameGrid()
    {
        int gridHeight = _gridMatrixLines.Length;
        int gridWidth = GetGameGridWidth();

        _absoluteGridWidth = (gridWidth - 1) * _ballSize * _spacing;
        _gameGrid.SetSize(gridWidth, gridHeight);

        for (int y = 0; y < gridHeight; y++)
        {
            char[] colors = _gridMatrixLines[y].Trim().ToCharArray();

            for (int x = 0; x < colors.Length; x++)
            {
                if (colors[x] == _emptyCellSymbol)
                {
                    continue;
                }

                Vector2 ballPosition = GetBallPosition(x, y);

                _ball = _ballCreator.CreateGameGridBall(ballPosition, colors[x], _gameGridTransform);

                _gameGrid.Add(_ball, x, y);
            }
        }
    }

    private int GetGameGridWidth()
    {
        int gridWidth = 0;
        int currentLineLength;

        foreach (string line in _gridMatrixLines)
        {
            currentLineLength = line.Trim().Length;

            if (currentLineLength > gridWidth)
            {
                gridWidth = currentLineLength;
            }
        }

        return gridWidth;
    }

    private Vector2 GetBallPosition(int gridPositionX, int gridPositionY)
    {
        float offsetX = (gridPositionY % 2 == 0) ? 0 : _ballSize * 0.5f * _spacing;

        Vector2 position = new Vector2(gridPositionX * _ballSize * _spacing + offsetX, - gridPositionY * _ballSize * _spacing * _hexHeightFactor);

        position.x -= _absoluteGridWidth / 2;
        position.y += _gameGridPositionY;

        return position;
    }    
}
