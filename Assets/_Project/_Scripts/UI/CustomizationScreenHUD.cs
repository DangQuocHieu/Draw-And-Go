using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomizationScreenHUD : Singleton<CustomizationScreenHUD>
{
    [Header("Button Prefab")]
    [SerializeField] private CarPartSelectionButton _selectionButtonPrefab;
    [SerializeField] private Button _homeButton;
    private List<CarPartSelectionButton> _allSelectionButtons = new List<CarPartSelectionButton>();

    [Header("Customization Button Container")]
    [SerializeField] private RectTransform _classicCarBodyContainer;
    [SerializeField] private RectTransform _classicCarWheelContainer;
    [Header("Selector")]
    [SerializeField] private RectTransform _carBodySelector;
    [SerializeField] private RectTransform _carWheelSelector;
    void Start()
    {
        SetUpAllSelectionButtons();
    }
    void OnEnable()
    {
        _homeButton.onClick.AddListener(() =>
        {
            DataManager.Instance.SaveGame(); //SAVE CUSTOMIZATION
            SceneManager.LoadSceneAsync(GameConstant.HOME_SCENE);
        });
       
    }

    void OnDisable()
    {
        _homeButton.onClick.RemoveAllListeners();

    }
    
    private void SetUpAllSelectionButtons()
    {
        List<CarPartSO> carBodyDatas = CarCustomizationManager.Instance.CarBodyDatas;
        List<CarPartSO> carWheelDatas = CarCustomizationManager.Instance.CarWheelDatas;
        SetUpSelectionButton(carBodyDatas, UnlockType.Classic, PartType.Body, _classicCarBodyContainer);
        SetUpSelectionButton(carWheelDatas, UnlockType.Classic, PartType.Wheel, _classicCarWheelContainer);
    }
    public void SetUpSelectionButton(List<CarPartSO> datas, UnlockType unlockType, PartType partType, RectTransform container)
    {
        List<CarPartSO> current = datas.Where(T => T.UnlockType == unlockType).ToList();
        foreach (var data in current)
        {
            CarPartSelectionButton button = Instantiate(_selectionButtonPrefab, container);
            button.Init(data, partType);
            _allSelectionButtons.Add(button);
        }
    }


    public void UpdateSelector(PartType type, Vector3 position)
    {
        switch (type)
        {
            case PartType.Body:
                _carBodySelector.GetComponent<RectTransform>().position = position;
                break;
            case PartType.Wheel:
                _carWheelSelector.GetComponent<RectTransform>().position = position;
                break;
        }
    }

}
