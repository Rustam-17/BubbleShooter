using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Button _gameplaySceneButton;
    [SerializeField] private Button _aboutSceneButton;

    private void OnEnable()
    {
        _gameplaySceneButton.onClick.AddListener(LoadGameplayScene);
        _aboutSceneButton.onClick.AddListener(LoadAboutScene);
    }

    private void OnDisable()
    {
        _gameplaySceneButton.onClick.RemoveListener(LoadGameplayScene);
        _aboutSceneButton.onClick.RemoveListener(LoadAboutScene);
    }

    private void LoadGameplayScene()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void LoadAboutScene()
    {
        SceneManager.LoadScene("AboutScene");
    }
}
