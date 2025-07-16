using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomLevelScreenHUD : MonoBehaviour, IMessageHandle
{
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private CustomLevelPanel _customLevelPanelPrefab;
    [SerializeField] private RectTransform _customLevelPanelContainer;

    void Start()
    {
        SetUpCustomLevelButton();
    }
    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnLevelRemoved, this);
        AddButtonListener();
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelRemoved, this);
        RemoveButtonListener();
    }

    private void AddButtonListener()
    {
        _createButton.onClick.AddListener(() =>
        {
            CustomLevelManager.Instance.CreateNewLevel();
            SceneManager.LoadSceneAsync(GameConstant.LEVEL_EDITOR_SCENE);
        });
        _homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(GameConstant.HOME_SCENE);
        });
    }

    private void RemoveButtonListener()
    {
        _createButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();
    }

    private void SetUpCustomLevelButton()
    {
        foreach (Transform child in _customLevelPanelContainer)
        {
            Destroy(child.gameObject);
        }
        List<CustomLevelData> createdLevels = CustomLevelManager.Instance.CreatedLevels;
        for (int i = 0; i < createdLevels.Count; i++)
        {
            var ui = Instantiate(_customLevelPanelPrefab, _customLevelPanelContainer);
            ui.Init(createdLevels[i], i);
        }
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelRemoved:
                SetUpCustomLevelButton();
                break;
        }
    }
}
