using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum LevelEditorTool
{
    DrawTool,
    EraseTool,
    MoveTool
}
public class LevelEditorManager : Singleton<LevelEditorManager>
{
    [SerializeField] private Transform _levelEnvironment;
    private Vector2 _mouseWorldPosition;
    [SerializeField] private Vector2 _gridPosition;
    [SerializeField] private Vector2 _previousObjectPosition;
    [SerializeField] private GameObject _currentObject;
    private LevelEditorTool _currentTool = LevelEditorTool.DrawTool;
    public LevelEditorTool CurrentTool => _currentTool;
    private LevelEditorTool _previousTool = LevelEditorTool.DrawTool;

    private Dictionary<Vector2, GameObject> _objectInstantiatedDictionary = new Dictionary<Vector2, GameObject>();
    public Dictionary<Vector2, GameObject> ObjectInstantiatedDictionary => _objectInstantiatedDictionary;

    [Header("Input State")]
    [SerializeField] private bool _isPressed = false;
    [SerializeField] private bool _isReleased = false;

    [Header("Intital Object")]
    private GameObject _carObject;
    private GameObject _endPointObject;

    void Start()
    {
        CustomLevelLoader.Instance.SetUpCustomLevel();
        _carObject = CustomLevelLoader.Instance.CarObject;
        _endPointObject = CustomLevelLoader.Instance.EndPointObject;
        SetUpInitialObject();
    }
    void Update()
    {
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _gridPosition = CalculateGridPosition(_mouseWorldPosition);
        HandleInput();
        ProcessCurrentTool();
    }

    public Vector2 CalculateGridPosition(Vector2 position)
    {
        return new Vector2(Mathf.FloorToInt(position.x) + 0.5f, Mathf.FloorToInt(position.y) + 0.5f);
    }

    private void SetUpInitialObject()
    {

        _objectInstantiatedDictionary.Add(_carObject.transform.position, _carObject);
        _objectInstantiatedDictionary.Add(_endPointObject.transform.position, _endPointObject);
    }
    private void ProcessCurrentTool()
    {
        switch (_currentTool)
        {
            case LevelEditorTool.DrawTool:
                HandleDrawTool();
                break;
            case LevelEditorTool.EraseTool:
                HandleEraseTool();
                break;
            case LevelEditorTool.MoveTool:
                HandleMoveTool();
                break;
        }
    }

    private void HandleDrawTool()
    {
        if (_currentObject != null)
        {
            _currentObject.transform.position = _gridPosition;
        }
        // if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        if (_isPressed)
        {
            if (_currentObject != null && !_objectInstantiatedDictionary.ContainsKey(_gridPosition))
            {
                GameObject objectInstantiated = Instantiate(_currentObject, _gridPosition, Quaternion.identity, _levelEnvironment);
                _objectInstantiatedDictionary.Add(_gridPosition, objectInstantiated);
            }
        }
    }
    private void HandleEraseTool()
    {
        // if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        if (_isPressed)
        {
            if (_objectInstantiatedDictionary.TryGetValue(_gridPosition, out GameObject objectInstantiated))
            {
                //Initial Object (Car and EndPoint) doesn't have PlaceableObject Component, don't erase
                if (objectInstantiated.GetComponent<PlaceableObject>() != null)
                {
                    Destroy(objectInstantiated);
                    _objectInstantiatedDictionary.Remove(_gridPosition);
                }

            }
        }
    }

    private void HandleMoveTool()
    {
        if (_isPressed)
        {
            if (_currentObject == null && _objectInstantiatedDictionary.TryGetValue(_gridPosition, out GameObject instantiatedObject))
            {
                _currentObject = instantiatedObject;
                _objectInstantiatedDictionary.Remove(_gridPosition);
                _previousObjectPosition = _gridPosition;
            }
            if (_currentObject != null)
            {
                _currentObject.transform.position = _gridPosition;
            }
        }
        else if (_isReleased && _currentObject != null)
        {
            Debug.Log("RELEASED");
            if (!_objectInstantiatedDictionary.ContainsKey(_gridPosition))
            {
                _objectInstantiatedDictionary.Add(_gridPosition, _currentObject);
            }
            else
            {
                _currentObject.transform.position = _previousObjectPosition;
                _objectInstantiatedDictionary.Add(_previousObjectPosition, _currentObject);
            }
            _currentObject = null;
        }
    }

    private void HandleInput()
    {
        _isPressed = Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject();
        _isReleased = Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject();
    }

    public void OnObjectSelected(GameObject objectSelected, string objectKey)
    {
        DestroyCurrentObject();
        _currentObject = Instantiate(objectSelected, _mouseWorldPosition, Quaternion.identity, _levelEnvironment);
        _currentObject.AddComponent<PlaceableObject>().ObjectKey = objectKey;
    }

    private void DestroyCurrentObject()
    {
        if (_currentObject != null)
        {
            Destroy(_currentObject); _currentObject = null;
        }
    }

    public void ChangeLevelEditorTool(LevelEditorTool tool)
    {
        DestroyCurrentObject();
        _currentTool = tool;
    }

    public bool ToggleEraseTool()
    {
        DestroyCurrentObject();
        if (_currentTool == LevelEditorTool.EraseTool)
        {
            //Undo Previous Tool
            _currentTool = _previousTool;
            //TURN OFF ERASE TOOL
            return false;
        }
        else
        {
            _previousTool = _currentTool;
            _currentTool = LevelEditorTool.EraseTool;
            //TURN ON ERASE TOOL
            return true;
        }
    }

    public LevelEditorTool ToggleDrawAndMoveTool()
    {
        _currentTool = (_currentTool == LevelEditorTool.DrawTool) ? LevelEditorTool.MoveTool : LevelEditorTool.DrawTool;
        return _currentTool;
    }

    public void SavePlaceableObjects()
    {
        CustomLevelData data = CustomLevelManager.Instance.CurrentLevelData;
        data.LevelObjectDatas = new List<LevelObjectData>();
        foreach (var placeableObject in _objectInstantiatedDictionary)
        {
            LevelObjectData objectData = new LevelObjectData();
            objectData.Position = placeableObject.Key;
            if (placeableObject.Value.TryGetComponent<PlaceableObject>(out var component))
            {
                objectData.ObjectKey = component.ObjectKey;
                data.LevelObjectDatas.Add(objectData);
            }

        }
        data.CarPosition = _carObject.transform.position;
        data.EndPointPosition = _endPointObject.transform.position;

    }



}
