using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawManager : Singleton<DrawManager>, IMessageHandle
{
    [SerializeField] private Transform _lineContainer;
    [SerializeField] private Line _linePrefab;
    private Line _currentLine;
    [SerializeField] private float _drawResolution = 0.1f;

    private Stack<Line> _lineStack = new Stack<Line>();

    [SerializeField] private float _maxLength;
    public float MaxLength => _maxLength;
    private float _remaining;
    public float Remaining => _remaining;

    [SerializeField] private float _currentLength;
    public float CurrentLength => _currentLength;

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnLevelSetUp, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelSetUp, this);
    }
    void Update()
    {
        HandleDrawLine();
    }

    private void HandleDrawLine()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_linePrefab, mouseWorldPos, Quaternion.identity, _lineContainer);

        }

        if (Input.GetMouseButton(0))
        {
            _currentLine.SetPosition(mouseWorldPos, _drawResolution);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentLine.PositionCount > 1)
            {
                _lineStack.Push(_currentLine);
            }
            else Destroy(_currentLine.gameObject);
            _currentLine = null;
        }
    }

    public void Undo()
    {

        if (_lineStack.Count == 0)
        {
            return;
        }
        Line line = _lineStack.Pop();
        _remaining += line.LineLength;
        _currentLength -= line.LineLength;
        if (_currentLength < 0) _currentLength = 0;
        _remaining = Mathf.Min(_remaining, _maxLength);
        Destroy(line.gameObject);
    }

    public void UpdateRemaining(float lengthToAdd)
    {
        _remaining -= lengthToAdd;
    }

    public void UpdateCurrentLength(float lengthToAdd)
    {
        _currentLength += lengthToAdd;
    }

    public float CalculateDrawRate()
    {
        return _remaining / _maxLength;
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelSetUp:
                LevelStat stat = (LevelStat)message.data[0];
                _maxLength = stat.MaxInk;
                _remaining = _maxLength;
                break;
        }
    }
}
