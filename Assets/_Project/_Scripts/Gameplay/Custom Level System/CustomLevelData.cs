using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelObjectData
{
    public string ObjectKey;
    public Vector3 Position;
    
}

[System.Serializable]
public class CustomLevelData
{
    public List<LevelObjectData> LevelObjectDatas = new List<LevelObjectData>();
    public Vector2 CarPosition = new Vector2(-6.5f, 0.5f);
    public Vector2 EndPointPosition = new Vector2(6.5f, 0.5f);
    public bool UseEngine = true;
    public string ScreenshotFileName;
}
