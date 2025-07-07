using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCollection", menuName = "Scriptable Objects/LevelCollection")]
public class LevelCollection : ScriptableObject
{
    public GameMode Mode;
    public List<LevelStat> LevelStats = new List<LevelStat>();
    public List<GameObject> LevelPrefabs;
#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < LevelStats.Count; i++)
        {
            LevelStats[i].LevelId = i;
            if (i < LevelPrefabs.Count)
            {
                LevelStats[i].LevelPrefab = LevelPrefabs[i];
            }
        }
    }
#endif
}

