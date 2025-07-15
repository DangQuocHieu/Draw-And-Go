using UnityEngine;
using UnityEngine.UI;

public class LevelEditorScreenHUD : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _engineButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _playButton;

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
        _eraseButton.onClick.AddListener(() => { });
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

}
