using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomLevelScreenHUD : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _homeButton;
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
        _createButton.onClick.AddListener(() =>
        {
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
}
