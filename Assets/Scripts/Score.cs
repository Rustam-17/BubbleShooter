using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    private int _count;
    private string _countFileName;

    public UnityEvent OnCountChanged;

    public int Count => _count;

    private void Start()
    {
        _countFileName = gameObject.name;

        _count = PlayerPrefs.GetInt(_countFileName);
        OnCountChanged?.Invoke();
    }

    public void Add(int volume)
    {
        _count += volume;

        PlayerPrefs.SetInt(_countFileName, _count);
        OnCountChanged?.Invoke();
    }

    public bool TryRemove(int volume)
    {
        if (_count < volume)
        {
            return false;
        }

        _count -= volume;

        PlayerPrefs.SetInt(_countFileName, _count);
        OnCountChanged?.Invoke();

        return true;
    }
}
