using UnityEngine;
using UnityEngine.UI;

public class UrlButton : MonoBehaviour
{
    [SerializeField] private string _url;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Application.OpenURL(_url);
    }
}
