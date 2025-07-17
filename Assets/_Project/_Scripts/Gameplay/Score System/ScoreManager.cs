using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>, IMessageHandle
{
    [SerializeField] private RectTransform _starUI;
    [SerializeField] private RectTransform _starContainer;
    [SerializeField] private float[] _starRates;
    private List<RectTransform> _starUIs = new List<RectTransform>();
    private float _rate;
    void Start()
    {
        SetUpStarUI();
    }
    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnCustomLevelSetUp, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnCustomLevelSetUp, this);
    }
    void Update()
    {
        UpdateStarUI();
    }
    private void SetUpStarUI()
    {
        foreach (var rate in _starRates)
        {
            var starUI = Instantiate(_starUI, _starContainer);
            starUI.anchorMin = new Vector2(rate, 0.5f);
            starUI.anchorMax = new Vector2(rate, 0.5f);
            starUI.anchoredPosition = Vector2.zero;
            _starUIs.Add(starUI);
        }
    }

    private void UpdateStarUI()
    {
        if (DrawManager.Instance == null) return;
        _rate = DrawManager.Instance.CalculateDrawRate();
        for (int i = 0; i < _starRates.Length; i++)
        {
            if (_rate < _starRates[i])
            {
                _starUIs[i].gameObject.SetActive(false);
            }
            else _starUIs[i].gameObject.SetActive(true);
        }
    }

    public int GetTotalScore()
    {
        int score = 0;
        for (int i = 0; i < _starRates.Length; i++)
        {
            if (_rate >= _starRates[i])
            {
                ++score;
            }
        }
        return score;
    }

    public void UpdateScoreList(GameMode gameMode, int levelIndex)
    {
        List<int> scoreList = DataManager.Instance.GetScoreList(gameMode);
        while (scoreList.Count < levelIndex + 1)
        {
            scoreList.Add(0);
        }
        int totalScore = GetTotalScore();
        int coinCollected = totalScore - scoreList[levelIndex];
        if (coinCollected < 0) coinCollected = 0;
        scoreList[levelIndex] = Mathf.Max(scoreList[levelIndex], totalScore);
        CurrencyManager.Instance.AddCoin(coinCollected);
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnCustomLevelSetUp:
                gameObject.SetActive(false);    
                break;
        }
    }
}
