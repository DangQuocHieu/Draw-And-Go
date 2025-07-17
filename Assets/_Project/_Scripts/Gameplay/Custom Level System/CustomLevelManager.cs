using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomLevelManager : PersistentSingleton<CustomLevelManager>, ISaveable
{
    [SerializeField] private List<CustomLevelData> _createdLevels = new List<CustomLevelData>();
    public List<CustomLevelData> CreatedLevels => _createdLevels;
    private CustomLevelData _currentLevelData;
    public CustomLevelData CurrentLevelData { get { return _currentLevelData; } set { _currentLevelData = value; } }

    private int _levelIndex;
    public int LevelIndex {get { return _levelIndex; } set{ _levelIndex = value; }}
    public void LoadData(PlayerData data)
    {
        _createdLevels = data.CreatedLevels;
    }

    public void SaveData(PlayerData data)
    {
        data.CreatedLevels = _createdLevels;
    }

    public void CreateNewLevel()
    {
        _currentLevelData = new CustomLevelData();
        _createdLevels.Add(_currentLevelData);
        Debug.Log("CREATE NEW LEVEL");
    }

    public void SaveCurrentLevel()
    {
        LevelEditorManager.Instance.SavePlaceableObjects();
    }

    public void RemoveLevel(CustomLevelData data)
    {
        _createdLevels.Remove(data);
        MessageManager.SendMessage(new Message(GameMessageType.OnLevelRemoved));
    }

    public bool ToggleEngine()
    {
        _currentLevelData.UseEngine = !_currentLevelData.UseEngine;
        return _currentLevelData.UseEngine;
    }

}
