using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridConstructor : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private int _height;
    [SerializeField] private int _width;

    private GameGrid _gameGrid;

    private void Start()
    {
        _gameGrid = new GameGrid(_height, _width);
    }

    private void CreateGameGrid()
    {

    }
}
