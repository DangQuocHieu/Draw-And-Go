using UnityEngine;

public enum UnlockType
{
    Classic, 
    Special
}
public abstract class CarPartSO : ScriptableObject
{
    public PartType PartType;
    public string PartID;
    public Sprite PartSprite;
    public UnlockType UnlockType;
    public int LevelToUnlock = -1;
}
