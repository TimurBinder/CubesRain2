using TMPro;
using UnityEngine;

public class SpawnCounter<T> : MonoBehaviour where T : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI _activeCountText;
    [SerializeField] private TextMeshProUGUI _totalCountText;
    [SerializeField] private Spawner<T> _spawner;

    private void OnEnable()
    {
        _spawner.ActiveCountChanged += SetActiveCount;
        _spawner.TotalCountChanged += SetTotalCount;
    }

    private void OnDisable()
    {
        _spawner.ActiveCountChanged -= SetActiveCount;
        _spawner.TotalCountChanged -= SetTotalCount;
    }

    private void SetActiveCount(int count)
    {
        _activeCountText.text = count.ToString();
    }

    private void SetTotalCount(int count)
    {
        _totalCountText.text = count.ToString();
    }
}
