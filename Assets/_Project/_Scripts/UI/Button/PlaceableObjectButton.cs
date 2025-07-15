using UnityEngine;
using UnityEngine.UI;
public class PlaceableObjectButton : MonoBehaviour
{
    [SerializeField] private PlaceableObjectData _data;
    public PlaceableObjectData Data => _data;
    [SerializeField] private Image _objectImage;

    public void Init(PlaceableObjectData data)
    {
        _data = data;
        _objectImage.sprite = data.Sprite;
    }

    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelEditorManager.Instance.OnObjectSelected(_data.Prefab);
        });
    }
}
