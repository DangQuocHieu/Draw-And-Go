using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private GameMode _currentMode;
    public GameMode CurrentMode {get { return _currentMode; } set { _currentMode = value; }}
}
