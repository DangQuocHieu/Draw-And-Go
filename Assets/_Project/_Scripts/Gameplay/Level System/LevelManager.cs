using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : PersistentSingleton<LevelManager>, ISaveable, IMessageHandle
{

    [SerializeField] private GameMode[] _gameModes;
    [SerializeField] private LevelCollection[] _levelCollections;
    private Dictionary<GameMode, LevelCollection> _levelDictionary = new Dictionary<GameMode, LevelCollection>();
    public Dictionary<GameMode, LevelCollection> LevelDictionary => _levelDictionary;
    private GameMode _currentMode;
    [SerializeField] private int _currentLevelIndex;
    public int CurrentLevelIndex { get { return _currentLevelIndex; } set { _currentLevelIndex = value; } }

    private int _levelReached;
    public int LevelReached => _levelReached;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Update()
    {
        _currentMode = GameManager.Instance.CurrentMode;
    }
    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnLevelCompleted, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelCompleted, this);
    }
    private void Init()
    {
        for (int i = 0; i < _gameModes.Length; i++)
        {
            _levelDictionary.Add(_gameModes[i], _levelCollections[i]);
        }
    }



    public LevelStat GetCurrentLevel()
    {
        return _levelDictionary[_currentMode].LevelStats[_currentLevelIndex];
    }

    public int GetLevelCount()
    {
        return _levelDictionary[_currentMode].LevelStats.Count;
    }
    public int GetLevelCount(GameMode gameMode)
    {
        if (_levelDictionary.ContainsKey(gameMode))
        {
            return _levelDictionary[gameMode].LevelStats.Count;
        }
        return -1;
    }

    public void SaveData(PlayerData data)
    {
        switch (_currentMode)
        {
            case GameMode.Draw:
                data.DrawLevelReached = _levelReached;
                break;
            case GameMode.Cut:
                data.CutLevelReached = _levelReached;
                break;
        }
    }

    public void LoadData(PlayerData data)
    {
        switch (_currentMode)
        {
            case GameMode.Draw:
                _levelReached = data.DrawLevelReached;
                break;
            case GameMode.Cut:
                _levelReached = data.CutLevelReached;
                break;
        }
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelCompleted:
                OnLevelCompleted();
                break;
        }
    }

    private void OnLevelCompleted()
    {
        ScoreManager.Instance.UpdateScoreList(_currentMode, _currentLevelIndex);
        UnlockNextLevel();
        DataManager.Instance.SaveGame();
        ItemUnlockManager.Instance.UnlockByLevel();
    }
    private void UnlockNextLevel()
    {
        int nextLevel = _currentLevelIndex + 1;
        _levelReached = Mathf.Max(nextLevel, _levelReached);
    }

    public void GoToNextLevel()
    {
        int maxLevelIndex = _levelDictionary[_currentMode].LevelStats.Count - 1;
        ++_currentLevelIndex;
        _currentLevelIndex = Mathf.Min(_currentLevelIndex, maxLevelIndex);
    }

}
