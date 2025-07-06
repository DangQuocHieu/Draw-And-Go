using UnityEngine;

[System.Serializable]
public class LevelStat
{
    public GameMode Mode;
    public int LevelId;
    public float MaxInk;
    public bool UseEngine;
    public Vector2 CarPosition;

    public GameObject LevelPrefab;
}
