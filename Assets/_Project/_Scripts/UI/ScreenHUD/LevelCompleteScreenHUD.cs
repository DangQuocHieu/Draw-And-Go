using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class LevelCompleteScreenHUD : MonoBehaviour
{
    [SerializeField] private RectTransform _starContainer;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextButton;

    void OnEnable()
    {
        AddButtonListener();
        DisplayStarUI();
    }

    void OnDisable()
    {
        RemoveButtonListener();
    }

    private void DisplayStarUI()
    {
        int score = ScoreManager.Instance.GetTotalScore();
        Debug.Log("SCORE: " + score);
        for (int i = 0; i < score; i++)
        {
            _starContainer.transform.GetChild(i).gameObject.SetActive(true);
        }

    }

    private void AddButtonListener()
    {
        _restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        });
        _homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(GameConstant.HOME_SCENE);
        });
        _nextButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.GoToNextLevel();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        });
    }

    private void RemoveButtonListener()
    {
        _restartButton.onClick.RemoveAllListeners();
        _homeButton.onClick.RemoveAllListeners();
        _nextButton.onClick.RemoveAllListeners();
    }
}
