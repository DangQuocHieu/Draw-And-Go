using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameMode _currentMode;
    public GameMode CurrentMode { get { return _currentMode; } set { _currentMode = value; } }

    private string _previousSceneName;
    public string PreviousSceneName {get { return _previousSceneName; } set { _previousSceneName = value; }}
}
