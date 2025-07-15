using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    public TextMeshProUGUI LevelText => _levelText;
    private int _levelIndex;
    [SerializeField] private RectTransform _starContainer;
    void OnEnable()
    {
        AddButtonListener();
    }

    void OnDisable()
    {
        RemoveButtonListener();
    }
    public void Init(int index)
    {
        _levelIndex = index;
        _levelText.text = (_levelIndex + 1).ToString();
    }


    private void AddButtonListener()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            // MessageManager.SendMessage(new Message(GameMessageType.OnUIButtonClicked));
            // MessageManager.SendMessage(new Message(GameMessageType.OnLevelButtonClicked, new object[] { _levelCount }));
            LevelManager.Instance.CurrentLevelIndex = _levelIndex;
            SceneManager.LoadSceneAsync(GameConstant.GAMEPLAY_SCENE);
        });
    }

    private void RemoveButtonListener()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void LockLevelButton()
    {
        GetComponent<Button>().interactable = false;
    }

    public void UnlockLevelButton()
    {
        GetComponent<Button>().interactable = true;
    }

    public void DisplayStarUI(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            _starContainer.GetChild(i).gameObject.SetActive(true);
        }
    }
}
