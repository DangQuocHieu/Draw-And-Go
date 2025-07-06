using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int DrawLevelReached;
    public int CutLevelReached;
    public int TotalCoin;
    public List<int> DrawLevelStars = new List<int>();
    public List<int> CutLevelStars = new List<int>();
    
}
