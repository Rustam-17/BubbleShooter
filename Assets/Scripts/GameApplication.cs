using UnityEngine;
using UnityEngine.UI;

public class GameApplication : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(Close);
    }

    private void Close()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}