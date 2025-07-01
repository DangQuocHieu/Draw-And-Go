using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCollection", menuName = "Scriptable Objects/LevelCollection")]
public class LevelCollection : ScriptableObject
{
    public GameMode Mode;
    public List<LevelStat> LevelStats = new List<LevelStat>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < LevelStats.Count; i++)
        {
            LevelStats[i].LevelId = i;
        }
    }
#endif
}

