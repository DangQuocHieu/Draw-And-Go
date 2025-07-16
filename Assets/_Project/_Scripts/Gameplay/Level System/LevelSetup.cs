using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    void Start()
    {
        SetUpLevel();
    }

    private void SetUpLevel()
    {
        var currentMode = GameManager.Instance.CurrentMode;
        if (currentMode == GameMode.Create)
        {
            CustomLevelLoader.Instance.SetUpCustomLevel();
            MessageManager.SendMessage(new Message(GameMessageType.OnCustomLevelSetUp));
        }
        else
        {
            SetUpBuiltInLevel();
        }
    }

    private void SetUpBuiltInLevel()
    {
        LevelStat currentLevel = LevelManager.Instance.GetCurrentLevel();
        Instantiate(currentLevel.LevelPrefab);
        MessageManager.SendMessage(new Message(GameMessageType.OnLevelSetUp, new object[] { currentLevel }));
    }
}
