using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
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
    
}
