using UnityEngine;

public enum UnlockType
{
    Classic, 
    Special
}
public abstract class CarPartSO : ScriptableObject
{
    public string PartID;
    public Sprite PartSprite;
    public UnlockType UnlockType;
}
