using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int DrawLevelReached;
    public int CutLevelReached;
    public int TotalCoin;
    public List<int> DrawLevelScores = new List<int>();
    public List<int> CutLevelScores = new List<int>();

    public string CarBodyID = "Hatchback";
    public string CarWheelID = "Plus Wheel";
    public List<string> UnlockedItems = new List<string>();
    
}
