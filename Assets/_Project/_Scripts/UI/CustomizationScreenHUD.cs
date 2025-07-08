using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomizationScreenHUD : MonoBehaviour
{
    [Header("Button Prefab")]
    [SerializeField] private CarPartSelectionButton _selectionButtonPrefab;

    [Header("Customization Button Container")]
    [SerializeField] private RectTransform _classicCarBodyContainer;
    [SerializeField] private RectTransform _classicCarWheelContainer;

    void Start()
    {

    }
    public void SetUpSelectionButton(List<CarPartSO> datas, UnlockType unlockType, RectTransform container)
    {
        List<CarPartSO> current = datas.Where(T => T.UnlockType == unlockType).ToList();
        foreach (var data in current)
        {
            CarPartSelectionButton button = Instantiate(_selectionButtonPrefab, container);
            button.Init(data);
        }
    }
}
