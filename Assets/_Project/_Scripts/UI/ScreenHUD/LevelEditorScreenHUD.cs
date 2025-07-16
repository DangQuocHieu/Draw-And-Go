using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditorScreenHUD : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _engineButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _drawToolButton;
    [SerializeField] private Button _moveToolButton;

    [Header("Placeable Object")]
    [SerializeField] private PlaceableObjectButton _placeableObjectButtonPrefab;
    [SerializeField] private RectTransform _placeableObjectButtonContainer;

    [Header("Button Sprite")]
    [SerializeField] private Sprite _eraseOnSprite;
    [SerializeField] private Sprite _eraseOffSprite;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _currentToolText;


    void Start()
    {
        SetUpPlaceableObjectButton();
        OnLevelEditorToolChange();
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
        _saveButton.onClick.AddListener(() =>
        {
            CustomLevelManager.Instance.SaveCurrentLevel();
        });
        _engineButton.onClick.AddListener(() => { });
        _homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(GameConstant.CUSTOM_LEVEL_SCENE);
        });
        _eraseButton.onClick.AddListener(() =>
        {
            OnEraseButtonClicked();
        });
        _playButton.onClick.AddListener(() => { });
        _drawToolButton.onClick.AddListener(() =>
        {
            OnDrawToolButtonClicked();
        });
        _moveToolButton.onClick.AddListener(() =>
        {
            OnMoveToolButtonClicked();
        });

    }

    private void RemoveButtonListener()
    {
        _saveButton.onClick.RemoveAllListeners();
        _engineButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();
        _eraseButton.onClick.RemoveAllListeners();
        _playButton.onClick.RemoveAllListeners();
        _drawToolButton.onClick.RemoveAllListeners();
        _moveToolButton.onClick.RemoveAllListeners();
    }

    private void SetUpPlaceableObjectButton()
    {
        List<PlaceableObjectData> datas = CustomLevelLoader.Instance.PlaceableObjectDatas;
        foreach (var data in datas)
        {
            PlaceableObjectButton button = Instantiate(_placeableObjectButtonPrefab, _placeableObjectButtonContainer);
            button.Init(data);
        }
    }

    private void OnEraseButtonClicked()
    {
        Image eraseImage = _eraseButton.transform.GetChild(0).GetComponent<Image>();
        if (LevelEditorManager.Instance.ToggleEraseTool())
        {
            eraseImage.sprite = _eraseOnSprite;
            _playButton.gameObject.SetActive(false);
            OnLevelEditorToolChange();
        }
        else
        {
            eraseImage.sprite = _eraseOffSprite;
            _playButton.gameObject.SetActive(true);
            OnLevelEditorToolChange();
        }
    }

    private void OnDrawToolButtonClicked()
    {
        LevelEditorManager.Instance.ChangeLevelEditorTool(LevelEditorTool.DrawTool);
        OnLevelEditorToolChange();
    }

    private void OnMoveToolButtonClicked()
    {
        LevelEditorManager.Instance.ChangeLevelEditorTool(LevelEditorTool.MoveTool);
        OnLevelEditorToolChange();
    }

    private void OnLevelEditorToolChange()
    {
        LevelEditorTool currentTool = LevelEditorManager.Instance.CurrentTool;
        switch (currentTool)
        {
            case LevelEditorTool.DrawTool:
                _drawToolButton.interactable = false;
                _moveToolButton.interactable = true;
                _currentToolText.text = GameConstant.DRAW_TOOL;
                _placeableObjectButtonContainer.gameObject.SetActive(true);
                break;
            case LevelEditorTool.MoveTool:
                _drawToolButton.interactable = true;
                _moveToolButton.interactable = false;
                _currentToolText.text = GameConstant.MOVE_TOOL;
                _placeableObjectButtonContainer.gameObject.SetActive(false);
                break;
            case LevelEditorTool.EraseTool:
                _drawToolButton.interactable = false;
                _moveToolButton.interactable = false;
                _currentToolText.text = GameConstant.ERASE_TOOL;
                _placeableObjectButtonContainer.gameObject.SetActive(false);
                break;
        }
    }

    



}
