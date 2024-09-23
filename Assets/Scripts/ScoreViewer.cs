using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TMP_Text _countText;

    private void OnEnable()
    {
        _score.OnCountChanged += UpdateScore;
    }

    private void OnDisable()
    {
        _score.OnCountChanged -= UpdateScore;
    }

    private void UpdateScore(int count)
    {
        _countText.text = count.ToString();
    }
}
