using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _countText;

    private void OnEnable()
    {
        UpdateScore();

        _score.OnCountChanged.AddListener(UpdateScore);
    }

    private void OnDisable()
    {
        _score.OnCountChanged.RemoveListener(UpdateScore);
    }

    private void UpdateScore()
    {
        _countText.text = _score.Count.ToString();
    }
}
