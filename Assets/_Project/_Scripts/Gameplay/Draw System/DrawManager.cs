using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
    [SerializeField] private Transform _lineContainer;
    [SerializeField] private Line _linePrefab;
    private Line _currentLine;
    [SerializeField] private float _drawResolution = 0.1f;

    private Stack<Line> _lineStack = new Stack<Line>();
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
            Debug.Log("Line Stack count is equal to 0");
            return;
        }
        Line line = _lineStack.Pop();
        Destroy(line.gameObject);
    }

}
