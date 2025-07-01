using System.Collections.Generic;
using UnityEngine;

public class LevelManager : PersistentSingleton<LevelManager>
{
    [SerializeField] private GameMode _currentMode;
    [SerializeField] private GameMode[] _gameModes;
    [SerializeField] private LevelCollection[] _levelCollections;
    private Dictionary<GameMode, LevelCollection> _levelDictionary = new Dictionary<GameMode, LevelCollection>();

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < _gameModes.Length; i++)
        {
            _levelDictionary.Add(_gameModes[i], _levelCollections[i]);
        }
    }
    private void SetUpLevel(GameMode mode, int currentLevel)
    {
        LevelStat current = _levelDictionary[mode].
    }
}
