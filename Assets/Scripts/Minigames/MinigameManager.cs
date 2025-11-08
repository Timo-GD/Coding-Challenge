using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject _hatPrefab;
    
    private static MinigameManager _instance;
    public static MinigameManager Instance => _instance;
    private int _count;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateMinigameCount()
    {
        _count++;
        
        if (_count == 3)
            WinPrize();
    }

    private void WinPrize()
    {
        GameObject prize = Instantiate(_hatPrefab, transform);
    }
}
