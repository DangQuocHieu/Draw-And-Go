using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionScreenHUD : MonoBehaviour
{
    [Header("Level Button")]
    private List<LevelButton> _allLevelButtons = new List<LevelButton>();
    private List<RectTransform> _allPages = new List<RectTransform>();
    [SerializeField] private RectTransform _levelButtonPrefab;

    [Header("Level Page")]
    [SerializeField] private RectTransform _scrollViewContent;  
    [SerializeField] private RectTransform _pagePrefab;
    [SerializeField] private int _levelsPerPage = 8;
    private int _maxPage;
    private int _currentPage = 0;
    [SerializeField] private float _pageStepX;
    [SerializeField] private float _duration;

    [Header("Page Button")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    void OnEnable()
    {
        AddButtonListener();
    }
    void Start()
    {
        InitializeLevelPages();
        InititalizeLevelButtons();
        DisplayStarUI();

    }

    void Update()
    {
        DisablePageButton();
    }

    void OnDisable()
    {
        RemoveButtonListener();
    }

    private void InitializeLevelPages()
    {
        int levelCount = LevelManager.Instance.GetLevelCount();
        _maxPage = Mathf.CeilToInt((float)levelCount / _levelsPerPage);
        for (int i = 0; i < _maxPage; i++)
        {
            RectTransform page = Instantiate(_pagePrefab, _scrollViewContent);
            _allPages.Add(page);
        }
    }
    private void InititalizeLevelButtons()
    {
        int levelCount = LevelManager.Instance.GetLevelCount();
        for (int i = 0; i < levelCount; i++)
        {
            LevelButton button = Instantiate(_levelButtonPrefab, _allPages[i / _levelsPerPage]).GetChild(0).GetComponent<LevelButton>();
            button.GetComponent<LevelButton>().Init(i);
            _allLevelButtons.Add(button);
        }

        int levelReached = LevelManager.Instance.LevelReached;
        for (int i = 0; i <= levelReached && i < levelCount; i++)
        {
            _allLevelButtons[i].GetComponent<LevelButton>().UnlockLevelButton();
        }
        for (int i = levelReached + 1; i < levelCount; i++)
        {
            _allLevelButtons[i].GetComponent<LevelButton>().LockLevelButton();
        }
    }

    private void DisplayStarUI()
    {
        List<int> starList = DataManager.Instance.GetScoreList(LevelManager.Instance.CurrentMode);
        for (int i = 0; i < starList.Count; i++)
        {
            _allLevelButtons[i].DisplayStarUI(starList[i]);
        }
    }

    private void GoNext()
    {
        ++_currentPage;
        _currentPage = Mathf.Clamp(_currentPage, 0, _maxPage - 1);
        MovePage();
    }

    private void GoBack()
    {
        --_currentPage;
        _currentPage = Mathf.Clamp(_currentPage, 0, _maxPage - 1);
        MovePage();
    }

    private void MovePage()
    {
        _scrollViewContent.DOAnchorPosX(_currentPage * _pageStepX, _duration).SetEase(Ease.Linear);
    }

    private void AddButtonListener()
    {
        _nextButton.onClick.AddListener(() =>
        {
            // MessageManager.SendMessage(new Message(GameMessageType.OnUIButtonClicked));
            GoNext();
        });

        _previousButton.onClick.AddListener(() =>
        {
            // MessageManager.SendMessage(new Message(GameMessageType.OnUIButtonClicked));
            GoBack();
        });
    }

    private void RemoveButtonListener()
    {
        _nextButton.onClick.RemoveAllListeners();
        _previousButton.onClick.RemoveAllListeners();
    }

    private void DisablePageButton()
    {
        _nextButton.interactable = _currentPage < _maxPage - 1;
        _previousButton.interactable = _currentPage > 0;
    }

}
