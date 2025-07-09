using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class CarCustomizationData
{
    public CarBodySO BodySO;
    public CarWheelSO WheelSO;
}
public class CarCustomizationManager : PersistentSingleton<CarCustomizationManager>, ISaveable
{
    [SerializeField] private List<CarPartSO> _carPartDatas = new List<CarPartSO>();
    private List<string> _unlockedItemIDs = new List<string>();
    public List<string> UnlockedItemIDs => _unlockedItemIDs;
    public List<CarPartSO> CarBodyDatas { get; private set; }
    public List<CarPartSO> CarWheelDatas { get; private set; }
    public List<CarPartSO> LockedPartDatas { get; private set; }

    [Header("Default Data")]
    [SerializeField] private CarPartSO _defaultCarBodyData;
    [SerializeField] private CarPartSO _defaultCarWheelData;



    [SerializeField] private CarCustomizationData _customizationData;
    public CarCustomizationData CustomizationData => _customizationData;
    public void CustomizeCar(PartType partType, CarPartSO data)
    {
        switch (partType)
        {
            case PartType.Body:
                CustomizeCarBody(data as CarBodySO);
                break;
            case PartType.Wheel:
                CustomizeCarWheel(data as CarWheelSO);
                break;
        }
    }

    public void CustomizeCarBody(CarBodySO data)
    {
        _customizationData.BodySO = data;
    }

    public void CustomizeCarWheel(CarWheelSO data)
    {
        _customizationData.WheelSO = data;
    }

    public void SaveData(PlayerData data)
    {
        data.CarBodyID = _customizationData.BodySO.PartID;
        data.CarWheelID = _customizationData.WheelSO.PartID;
    }

    public void LoadData(PlayerData data)
    {
        _unlockedItemIDs = data.UnlockedItems;
        if (_unlockedItemIDs.Count == 0)
        {
            _unlockedItemIDs.Add(_defaultCarBodyData.PartID);
            _unlockedItemIDs.Add(_defaultCarWheelData.PartID);
        }

            _customizationData.BodySO = _carPartDatas.Where(T => T.PartID == data.CarBodyID).FirstOrDefault() as CarBodySO;
        _customizationData.WheelSO = _carPartDatas.Where(T => T.PartID == data.CarWheelID).FirstOrDefault() as CarWheelSO;
        MessageManager.SendMessage(new Message(GameMessageType.OnCarCustomizationLoaded));
        InitializeCarPartList();
    }

    private void InitializeCarPartList()
    {
        CarBodyDatas = _carPartDatas.Where(T => T.PartType == PartType.Body).ToList();
        CarWheelDatas = _carPartDatas.Where(T => T.PartType == PartType.Wheel).ToList();
        LockedPartDatas = _carPartDatas.Where(T => !_unlockedItemIDs.Contains(T.PartID)).ToList();
    }

    public bool IsItemUnlocked(string id)
    {
        return _unlockedItemIDs.Contains(id);
    }
}
