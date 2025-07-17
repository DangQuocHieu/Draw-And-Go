using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomLevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _updateButton;
    [SerializeField] private Button _removeButton;

    private CustomLevelData _customLevelData;
    private int _levelIndex;

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
        _levelButton.onClick.AddListener(() =>
        {
            CustomLevelManager.Instance.CurrentLevelData = _customLevelData;
            CustomLevelManager.Instance.LevelIndex = _levelIndex;
            GameManager.Instance.PreviousSceneName = GameConstant.CUSTOM_LEVEL_SCENE;
            SceneManager.LoadSceneAsync(GameConstant.GAMEPLAY_SCENE);
        });
        _removeButton.onClick.AddListener(() =>
        {
            CustomLevelManager.Instance.RemoveLevel(_customLevelData);
            Debug.Log("REMOVE LEVEL");
        });

        _updateButton.onClick.AddListener(() =>
        {
            CustomLevelManager.Instance.CurrentLevelData = _customLevelData;
            CustomLevelManager.Instance.LevelIndex = _levelIndex;
            GameManager.Instance.PreviousSceneName = GameConstant.CUSTOM_LEVEL_SCENE;
            SceneManager.LoadSceneAsync(GameConstant.LEVEL_EDITOR_SCENE);
        });
    }

    private void RemoveButtonListener()
    {
        _levelButton.onClick.RemoveAllListeners();
        _updateButton.onClick.RemoveAllListeners();
        _removeButton.onClick.RemoveAllListeners();
    }


    public void Init(CustomLevelData data, int index)
    {
        _customLevelData = data;
        _levelIndex = index;
        _levelText.text = "Level " + (_levelIndex + 1);
    }
}
