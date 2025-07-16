using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableObjectData", menuName = "Scriptable Objects/PlaceableObjectData")]
public class PlaceableObjectData : ScriptableObject
{
    public string ObjectKey;
    public Sprite Sprite;
    public GameObject Prefab;
}
