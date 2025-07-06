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
        LevelStat currentLevel = LevelManager.Instance.GetCurrentLevel();
        Instantiate(currentLevel.LevelPrefab);
        MessageManager.SendMessage(new Message(GameMessageType.OnLevelSetUp, new object[] { currentLevel }));
    }
}
