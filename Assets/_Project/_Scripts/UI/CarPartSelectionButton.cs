using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Unity.Android.Gradle.Manifest;
using UnityEditor.SearchService;

public class CarPartSelectionButton : MonoBehaviour
{
    [SerializeField] private PartType _partType;
    private CarPartSO _partSO;
    [SerializeField] private Image _carPartImage;
    private bool _isUnlocked;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => _partSO != null && DataManager.Instance.Data != null);
        if (IsSaved())
        {
            CustomizationScreenHUD.Instance.UpdateSelector(_partType, GetComponent<RectTransform>().position);
        }
    }

    void Update()
    {
        _isUnlocked = CarCustomizationManager.Instance.IsItemUnlocked(_partSO.PartID);
        GetComponent<Button>().interactable = _isUnlocked;
        _carPartImage.enabled = _isUnlocked;
        
    }
    public void Init(CarPartSO data, PartType partType)
    {
        _partSO = data;
        _carPartImage.sprite = _partSO.PartSprite;
        _partType = partType;
        AddButtonListener();
        // if (!data.IsUnlocked)
        // {
        //     GetComponent<Button>().interactable = false;
        //     _carPartImage.enabled = false;
        // }
    }
    void OnDisable()
    {
        RemoveButtonListener();
    }
    private void AddButtonListener()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            CarCustomizationManager.Instance.CustomizeCar(_partType, _partSO);
            CustomizationScreenHUD.Instance.UpdateSelector(_partType, GetComponent<RectTransform>().position);
        });
    }

    private void RemoveButtonListener()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private bool IsSaved()
    {
        switch (_partType)
        {
            case PartType.Body:
                return DataManager.Instance.Data.CarBodyID == _partSO.PartID;
            case PartType.Wheel:
                return DataManager.Instance.Data.CarWheelID == _partSO.PartID;
        }
        return false;
    }

    

}

