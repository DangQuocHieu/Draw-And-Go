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
    [SerializeField] private TextMeshProUGUI _totalLengthText;
    [SerializeField] private Slider _inkSlider;


    void OnEnable()
    {
        AddButtonListener();
        MessageManager.AddSubscriber(GameMessageType.OnLevelSetUp, this);
    }

    void OnDisable()
    {
        RemoveButtonListener();
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelSetUp, this);
    }

    void Update()
    {
        UpdateInkSlider();
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
            SceneManager.LoadSceneAsync(GameConstant.HOME_SCENE);
        });
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
        _inkSlider.value = DrawManager.Instance.Remaining;
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelSetUp:
                LevelStat stat = (LevelStat)message.data[0];
                SetUpInkSlider(stat.MaxInk);
                break;
        }
    }

    private void SetUpInkSlider(float maxValue)
    {
        _inkSlider.minValue = 0;
        _inkSlider.maxValue = maxValue;
        _inkSlider.value = maxValue;
    }
}
