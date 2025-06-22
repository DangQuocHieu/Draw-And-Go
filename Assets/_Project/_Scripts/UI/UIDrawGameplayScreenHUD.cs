using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDrawGameplayScreenHUD : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _restartButton;

    void OnEnable()
    {
        AddButtonListener();        
    }

    void OnDisable()
    {
        RemoveButtonListener();
    }

    void Update()
    {
      
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
    }

    private void RemoveButtonListener()
    {
        _startButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _eraseButton.onClick.RemoveAllListeners();
    }

    private void OnStartButtonClicked()
    {
        MessageManager.SendMessage(new Message(GameMessageType.OnCarStarted));
        _startButton.gameObject.SetActive(false);
        _eraseButton.gameObject.SetActive(false);
    }
}
