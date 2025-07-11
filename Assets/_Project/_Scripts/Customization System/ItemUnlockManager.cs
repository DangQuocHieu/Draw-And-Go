using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemUnlockManager : PersistentSingleton<ItemUnlockManager>, IMessageHandle
{
    [SerializeField] private int _itemPrice = 10;

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnLevelCompleted, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelCompleted, this);
    }

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
        CarPartSO classicItem = classic[Random.Range(0, classic.Count)];
        DataManager.Instance.Data.UnlockedItems.Add(classicItem.PartID);
        lockedItem.Remove(classicItem);
        MessageManager.SendMessage(new Message(GameMessageType.OnItemUnlocked, new object[] { classicItem }));
        CurrencyManager.Instance.AddCoin(-_itemPrice);
        // DataManager.Instance.SaveGame();
    }

    public void UnlockByLevel()
    {
        int totalLevel = DataManager.Instance.GetTotalLevelReached();
        List<CarPartSO> lockedItem = CarCustomizationManager.Instance.LockedPartDatas;
        CarPartSO specialItem = lockedItem.Where(T => T.UnlockType == UnlockType.Special && totalLevel == T.LevelToUnlock).FirstOrDefault();
        if (specialItem != null)
        {
            DataManager.Instance.Data.UnlockedItems.Add(specialItem.PartID);
            lockedItem.Remove(specialItem);
            // DataManager.Instance.SaveGame();
        }
    }



    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelCompleted:
                break;
        }
    }
}
