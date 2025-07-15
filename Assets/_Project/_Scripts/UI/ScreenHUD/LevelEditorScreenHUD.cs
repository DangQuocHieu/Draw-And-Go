using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelEditorScreenHUD : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _engineButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _playButton;

    [Header("Placeable Object")]
    [SerializeField] private PlaceableObjectButton _placeableObjectButtonPrefab;
    [SerializeField] private RectTransform _placeableObjectButtonContainer;

    [Header("Button Sprite")]
    [SerializeField] private Sprite _eraseOnSprite;
    [SerializeField] private Sprite _eraseOffSprite;

    [SerializeField] private Sprite _drawToolSprite;
    [SerializeField] private Sprite _moveToolSprite;

    void Start()
    {
        SetUpPlaceableObjectButton();
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
        _saveButton.onClick.AddListener(() => { });
        _engineButton.onClick.AddListener(() => { });
        _homeButton.onClick.AddListener(() => { });
        _eraseButton.onClick.AddListener(() =>
        {
            OnEraseButtonClick();
        });
        _playButton.onClick.AddListener(() => { });

    }

    private void RemoveButtonListener()
    {
        _saveButton.onClick.RemoveAllListeners();
        _engineButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();
        _eraseButton.onClick.RemoveAllListeners();
        _playButton.onClick.RemoveAllListeners();
    }

    private void SetUpPlaceableObjectButton()
    {
        List<PlaceableObjectData> datas = LevelEditorManager.Instance.PlaceableObjectDatas;
        foreach (var data in datas)
        {
            PlaceableObjectButton button = Instantiate(_placeableObjectButtonPrefab, _placeableObjectButtonContainer);
            button.Init(data);
        }
    }

    private void OnEraseButtonClick()
    {
        Image eraseImage = _eraseButton.transform.GetChild(0).GetComponent<Image>();
        if (LevelEditorManager.Instance.ToggleEraseTool())
        {
            eraseImage.sprite = _eraseOnSprite;
            _placeableObjectButtonContainer.gameObject.SetActive(false);
            _playButton.gameObject.SetActive(false);
        }
        else
        {
            eraseImage.sprite = _eraseOffSprite;
            _placeableObjectButtonContainer.gameObject.SetActive(true);
            _playButton.gameObject.SetActive(true);
        }
    }
}
