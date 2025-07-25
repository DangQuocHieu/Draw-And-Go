using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomLevelLoader : PersistentSingleton<CustomLevelLoader>
{
    [SerializeField] private List<PlaceableObjectData> _placeableObjectDatas;
    public List<PlaceableObjectData> PlaceableObjectDatas => _placeableObjectDatas;
    private Dictionary<string, GameObject> _placeableObjectDictionary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> PlaceableObjectDictionary => _placeableObjectDictionary;
    [Header("Initial Object Prefab")]
    [SerializeField] private GameObject _carPrefab;
    [SerializeField] private GameObject _endPointPrefab;
    private Transform _levelEnvironment;

    public GameObject CarObject { get; private set; }
    public GameObject EndPointObject { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        foreach (var data in _placeableObjectDatas)
        {
            _placeableObjectDictionary.Add(data.ObjectKey, data.Prefab);
        }
    }

    public void SetUpCustomLevel()
    {
        Dictionary<Vector2, GameObject> objectInstantiated = null;
        if (LevelEditorManager.Instance != null)
        {
            objectInstantiated = LevelEditorManager.Instance.ObjectInstantiatedDictionary;
            objectInstantiated.Clear();
        }
        CustomLevelData data = CustomLevelManager.Instance.CurrentLevelData;
        if (data != null)
        {
            if (_levelEnvironment != null) Destroy(_levelEnvironment.gameObject);
            _levelEnvironment = new GameObject("Level Environment").transform;
            foreach (var item in data.LevelObjectDatas)
            {
                if (_placeableObjectDictionary.TryGetValue(item.ObjectKey, out var go))
                {
                    GameObject instantiatedObject = Instantiate(go, item.Position, Quaternion.identity, _levelEnvironment);
                    instantiatedObject.AddComponent<PlaceableObject>().ObjectKey = item.ObjectKey;
                    if (objectInstantiated != null)
                    {
                        objectInstantiated.Add(item.Position, instantiatedObject);
                    }
                }
            }
            CarObject = Instantiate(_carPrefab, data.CarPosition, Quaternion.identity, _levelEnvironment);
            EndPointObject = Instantiate(_endPointPrefab, data.EndPointPosition, Quaternion.identity, _levelEnvironment);
            MessageManager.SendMessage(new Message(GameMessageType.OnCustomLevelSetUp, new object[] { data }));
        }
    }   

}
