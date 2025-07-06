using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _drawLevelText;
    [SerializeField] private TextMeshProUGUI _cutLevelText;

    [Header("Buttons")]
    [SerializeField] private Button _drawButton;
    [SerializeField] private Button _cutButton;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _customizeButton;

    void OnEnable()
    {
        AddButtonListener();
    }
    void Start()
    {
        SetLevelText();
    }

    void OnDisable()
    {
        RemoveButtonListener();
    }

    private void SetLevelText()
    {
        _drawLevelText.text = "Level " + (DataManager.Instance.Data.DrawLevelReached + 1).ToString();
        _cutLevelText.text = "Level " + (DataManager.Instance.Data.CutLevelReached + 1).ToString();
    }

    private void AddButtonListener()
    {
        _drawButton.onClick.AddListener(() =>
        {
            OnDrawButtonClicked();
        });
        _cutButton.onClick.AddListener(() =>
        {
            OnCutButtonClicked();
        });
    }

    private void OnDrawButtonClicked()
    {
        LevelManager.Instance.CurrentMode = GameMode.Draw;
        SceneManager.LoadSceneAsync(GameConstant.LEVEL_SELECTION_SCENE);
    }

    private void OnCutButtonClicked()
    {
        LevelManager.Instance.CurrentMode = GameMode.Cut;
        SceneManager.LoadSceneAsync(GameConstant.LEVEL_SELECTION_SCENE);
    }

    private void RemoveButtonListener()
    {
        _drawButton.onClick.RemoveAllListeners();
        _cutButton.onClick.RemoveAllListeners();
    }
}
