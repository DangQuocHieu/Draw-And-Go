using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public enum LevelEditorTool
{
    DrawTool,
    EraseTool,
    MoveTool
}
public class LevelEditorManager : Singleton<LevelEditorManager>
{
    [SerializeField] private List<PlaceableObjectData> _placeableObjectDatas;
    public List<PlaceableObjectData> PlaceableObjectDatas => _placeableObjectDatas;
    [SerializeField] private Transform _levelEnvironment;
    private Vector2 _mouseWorldPosition;
    private Vector2 _objectPosition;
    [SerializeField] private GameObject _currentObject;
    private LevelEditorTool _currentTool = LevelEditorTool.DrawTool;
    private LevelEditorTool _previousTool = LevelEditorTool.DrawTool;

    private Dictionary<Vector2, GameObject> _objectInstantiatedDictionary = new Dictionary<Vector2, GameObject>();


    void Update()
    {
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _objectPosition = new Vector2(Mathf.FloorToInt(_mouseWorldPosition.x) + 0.5f, Mathf.FloorToInt(_mouseWorldPosition.y) + 0.5f);
        HandleCustomLevel();
    }

    private void HandleCustomLevel()
    {
        switch (_currentTool)
        {
            case LevelEditorTool.DrawTool:
                HandleDrawTool();
                break;
            case LevelEditorTool.EraseTool:
                HandleMoveTool();
                break;
            case LevelEditorTool.MoveTool:
                break;
        }
    }

    private void HandleDrawTool()
    {
        if (_currentObject != null)
        {
            _currentObject.transform.position = _objectPosition;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_currentObject != null && !_objectInstantiatedDictionary.ContainsKey(_objectPosition))
            {
                GameObject objectInstantiated = Instantiate(_currentObject, _objectPosition, Quaternion.identity, _levelEnvironment);
                _objectInstantiatedDictionary.Add(_objectPosition, objectInstantiated);
            }
        }
    }
    private void HandleEraseTool()
    {
        if (_objectInstantiatedDictionary.TryGetValue(_objectPosition, out GameObject objectInstantiated))
        {
            Destroy(objectInstantiated);
            _objectInstantiatedDictionary.Remove(_objectPosition);
        }
    }


    private void HandleMoveTool()
    {

    }

    public void OnObjectSelected(GameObject objectSelected)
    {
        DestroyCurrentObject();
        _currentObject = Instantiate(objectSelected, _mouseWorldPosition, Quaternion.identity, _levelEnvironment);
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

}
