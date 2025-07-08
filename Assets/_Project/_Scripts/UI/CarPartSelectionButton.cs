using UnityEngine.UI;
using UnityEngine;

public class CarPartSelectionButton : MonoBehaviour
{
    [SerializeField] private PartType _partType;
    private CarPartSO _partSO;
    [SerializeField] private Image _carPartImage;

    public void Init(CarPartSO data)
    {
        _partSO = data;
        _carPartImage.sprite = _partSO.PartSprite;
    }
    void OnEnable()
    {
        AddButtonListener();
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
        });
    }

    private void RemoveButtonListener()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
