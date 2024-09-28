using UnityEngine;
using System;

public abstract class Count : MonoBehaviour
{
    private int _volume;
    private string _countFileName;

    public int Volume => _volume;

    public event Action<int> OnCountChanged;

    private void Start()
    {
        _countFileName = gameObject.name;

        //_volume = PlayerPrefs.GetInt(_countFileName);
        OnCountChanged?.Invoke(_volume);
    }

    public void Add(int volume)
    {
        _volume += volume;

        //PlayerPrefs.SetInt(_countFileName, _volume);
        OnCountChanged?.Invoke(_volume);
    }

    public bool TrySubtract(int volume)
    {
        if (_volume < volume)
        {
            return false;
        }

        _volume -= volume;

        //PlayerPrefs.SetInt(_countFileName, _volume);
        OnCountChanged?.Invoke(_volume);

        return true;
    }
}
