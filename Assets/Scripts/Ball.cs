using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color
{
    Red,
    Green,
    Yellow,
    Cian
}

public class Ball : MonoBehaviour
{
    private Color _color;

    public Color Color => _color;

    public void SetColor(Color color)
    {
        _color = color;
    }
}
