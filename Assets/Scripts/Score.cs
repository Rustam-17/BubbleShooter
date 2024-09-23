using UnityEngine;
using System;

public class Score : MonoBehaviour
{
    private int _count;
    private string _countFileName;

    public event Action<int> OnCountChanged;

    private void Start()
    {
        _countFileName = gameObject.name;

        _count = PlayerPrefs.GetInt(_countFileName);
        OnCountChanged?.Invoke(_count);
    }

    public void Add(int volume)
    {
        _count += volume;

        PlayerPrefs.SetInt(_countFileName, _count);
        OnCountChanged?.Invoke(_count);
    }

    public bool TryRemove(int volume)
    {
        if (_count < volume)
        {
            return false;
        }

        _count -= volume;

        PlayerPrefs.SetInt(_countFileName, _count);
        OnCountChanged?.Invoke(_count);

        return true;
    }
}
