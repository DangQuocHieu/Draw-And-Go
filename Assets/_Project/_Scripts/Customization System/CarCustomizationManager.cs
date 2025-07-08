using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CarCustomizationData
{
    public CarBodySO BodySO;
    public CarWheelSO WheelSO;
}
public class CarCustomizationManager : PersistentSingleton<CarCustomizationManager>
{
    [SerializeField] private List<CarPartSO> _carBodyDatas = new List<CarPartSO>();
    public List<CarPartSO> CarBodyDatas => _carBodyDatas;
    [SerializeField] private List<CarPartSO> _carWheelDatas = new List<CarPartSO>();
    public List<CarPartSO> CarWheelDatas => _carWheelDatas;

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
}
