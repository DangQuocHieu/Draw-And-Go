using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _drawLevelText;
    [SerializeField] private TextMeshProUGUI _cutLevelText;
    [SerializeField] private TextMeshProUGUI _coinText;

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

    void Update()
    {
        UpdateCoinText();
    }

    void OnDisable()
    {
        RemoveButtonListener();
    }

    private void SetLevelText()
    {
        int drawLevelCount = LevelManager.Instance.GetLevelCount(GameMode.Draw);
        int cutLevelCount = LevelManager.Instance.GetLevelCount(GameMode.Cut);
        _drawLevelText.text = "Level " + Mathf.Min(drawLevelCount, DataManager.Instance.Data.DrawLevelReached + 1).ToString();
        _cutLevelText.text = "Level " + Mathf.Min(cutLevelCount, DataManager.Instance.Data.CutLevelReached + 1).ToString();
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

    private void UpdateCoinText()
    {
        _coinText.text = CurrencyManager.Instance.TotalCoin.ToString();
    }
}
