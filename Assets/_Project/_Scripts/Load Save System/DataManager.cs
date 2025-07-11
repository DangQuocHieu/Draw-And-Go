using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : PersistentSingleton<DataManager>
{
    [SerializeField] private PlayerData _data = new PlayerData();
    public PlayerData Data => _data;

    [SerializeField] private string _saveName;

    private List<ISaveable> _saveables = new List<ISaveable>();

    void Start()
    {
        _saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>().ToList();
        SaveSystem.Initialize(_saveName);
        LoadGame();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        // LoadGame();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveGame();
        }
    }
    public void SaveGame()
    {
        foreach (var saveable in _saveables)
        {
            saveable.SaveData(_data);
        }
        string json = JsonUtility.ToJson(_data);
        SaveSystem.SetString(_saveName, json);
        SaveSystem.SaveToDisk();
    }

    public void LoadGame()
    {
        string json = SaveSystem.GetString(_saveName, "");
        _data = JsonUtility.FromJson<PlayerData>(json);
        if (_data == null) _data = new PlayerData();
        foreach (var saveable in _saveables)
        {
            saveable.LoadData(_data);
        }
    }

    public List<int> GetScoreList(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.Draw:
                return _data.DrawLevelScores;
            case GameMode.Cut:
                return _data.CutLevelScores;
        }
        return new List<int>();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause) SaveGame();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    public int GetTotalLevelReached()
    {
        return _data.DrawLevelReached + _data.CutLevelReached;
    }


}
