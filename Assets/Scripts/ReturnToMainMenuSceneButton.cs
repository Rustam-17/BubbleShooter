using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuSceneButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Return);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Return);
    }

    private void Return()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
