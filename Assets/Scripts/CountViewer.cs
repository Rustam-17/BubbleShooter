using TMPro;
using UnityEngine;

public class CountViewer : MonoBehaviour
{
    [SerializeField] private Count _count;
    [SerializeField] private TMP_Text _countText;

    private void OnEnable()
    {
        _count.OnCountChanged += UpdateCount;
    }

    private void OnDisable()
    {
        _count.OnCountChanged -= UpdateCount;
    }

    private void UpdateCount(int count)
    {
        _countText.text = count.ToString();
    }
}
