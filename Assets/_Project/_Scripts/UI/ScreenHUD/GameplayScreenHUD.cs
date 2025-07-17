using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayScreenHUD : MonoBehaviour, IMessageHandle
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _helpButton;
    [SerializeField] private TextMeshProUGUI _totalLengthText;
    [SerializeField] private Slider _inkSlider;

    [SerializeField] private RectTransform _engineOffRect;
    [SerializeField] private TextMeshProUGUI _levelText;


    void OnEnable()
    {
        AddButtonListener();
        MessageManager.AddSubscriber(GameMessageType.OnLevelSetUp, this);
        MessageManager.AddSubscriber(GameMessageType.OnCustomLevelSetUp, this);
    }

    void OnDisable()
    {
        RemoveButtonListener();
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelSetUp, this);
        MessageManager.RemoveSubscriber(GameMessageType.OnCustomLevelSetUp, this);
    }
    void Start()
    {
        if (GameManager.Instance.CurrentMode == GameMode.Create)
        {
            _levelText.text = "Level " + (CustomLevelManager.Instance.LevelIndex + 1).ToString();
        }
        else
        {
            if (LevelManager.Instance != null)
                _levelText.text = "Level " + (LevelManager.Instance.CurrentLevelIndex + 1).ToString();
        }

    }
    void Update()
    {
        UpdateInkSlider();
        if (DrawManager.Instance != null)
        {
            _totalLengthText.text = DrawManager.Instance.CurrentLength.ToString("F3");
        }
    }
    private void AddButtonListener()
    {
        _startButton.onClick.AddListener(() =>
        {
            OnStartButtonClicked();
        });
        _restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        });
        _eraseButton.onClick.AddListener(() =>
        {
            DrawManager.Instance.Undo();
        });
        _homeButton.onClick.AddListener(() =>
        {
            OnHomeButtonClicked();
        });
    }

    private void OnHomeButtonClicked()
    {
        GameMode currentMode = GameManager.Instance.CurrentMode;
        if (currentMode == GameMode.Create)
        {
            SceneManager.LoadSceneAsync(GameManager.Instance.PreviousSceneName);
        }
        else SceneManager.LoadSceneAsync(GameConstant.HOME_SCENE);
    }

    private void RemoveButtonListener()
    {
        _startButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _eraseButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();
    }

    private void OnStartButtonClicked()
    {
        MessageManager.SendMessage(new Message(GameMessageType.OnCarStarted));
        _startButton.gameObject.SetActive(false);
        _eraseButton.gameObject.SetActive(false);
    }


    private void UpdateInkSlider()
    {
        if (DrawManager.Instance == null) return;
        _inkSlider.value = DrawManager.Instance.Remaining;
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelSetUp:
                LevelStat stat = (LevelStat)message.data[0];
                SetUpInkSlider(stat.MaxInk);
                if (!stat.UseEngine)
                {
                    _engineOffRect.gameObject.SetActive(true);
                }
                break;
            case GameMessageType.OnCustomLevelSetUp:
                CustomLevelData data = (CustomLevelData)message.data[0];
                OnCustomLevelSetUp(data);
                break;

        }
    }

    private void OnCustomLevelSetUp(CustomLevelData data)
    {
        if (!data.UseEngine)
        {
            _engineOffRect.gameObject.SetActive(true);
        }
        _inkSlider.gameObject.SetActive(false);
        _helpButton.gameObject.SetActive(false);
        _totalLengthText.gameObject.SetActive(true);

    }

    private void SetUpInkSlider(float maxValue)
    {
        _inkSlider.minValue = 0;
        _inkSlider.maxValue = maxValue;
        _inkSlider.value = maxValue;
    }
}
