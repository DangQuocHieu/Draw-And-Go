using UnityEngine;

public class CurrencyManager : PersistentSingleton<CurrencyManager>, ISaveable
{
    [SerializeField] private int _totalCoin;
    public int TotalCoin => _totalCoin;

    public void AddCoin(int coinToAdd)
    {
        _totalCoin += coinToAdd;
    }

    public void LoadData(PlayerData data)
    {
        _totalCoin = data.TotalCoin;
    }

    public void SaveData(PlayerData data)
    {
        data.TotalCoin = _totalCoin;
    }
}
