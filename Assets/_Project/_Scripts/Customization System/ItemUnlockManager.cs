using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemUnlockManager : Singleton<ItemUnlockManager>
{
    [SerializeField] private int _itemPrice = 10;
    public int ItemPrice => _itemPrice;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnlockByCoin();
        }
    }
    public void UnlockByCoin()
    {
        int totalCoin = CurrencyManager.Instance.TotalCoin;
        if (totalCoin < _itemPrice) return;
        List<CarPartSO> lockedItem = CarCustomizationManager.Instance.LockedPartDatas;
        List<CarPartSO> classic = lockedItem.Where(T => T.UnlockType == UnlockType.Classic).ToList();
        //Classic Unlock Type: By Coin
        if (lockedItem.Count == 0) return;

        CarPartSO rand = classic[Random.Range(0, classic.Count)];
        DataManager.Instance.Data.UnlockedItems.Add(rand.PartID);
        lockedItem.Remove(rand);
        CurrencyManager.Instance.AddCoin(-_itemPrice);
        //SEND MESSAGE : DISPLAY SOME UI,...
        DataManager.Instance.SaveGame();
    }

    public void UnlockByLevel()
    {

    }
}
