using UnityEngine;

public interface ISaveable
{
    public void SaveData(PlayerData data);

    public void LoadData(PlayerData data);
}
