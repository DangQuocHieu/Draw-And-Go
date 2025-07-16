using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private string _objectKey;
    public string ObjectKey {get { return _objectKey;} set { _objectKey = value; }}
}
