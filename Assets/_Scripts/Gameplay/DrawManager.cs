using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Transform _lineContainer;
    [SerializeField] private Line _linePrefab;
    private Line _currentLine;
    [SerializeField] private float _drawResolution = 0.1f;

    private Stack<Line> _lineStack = new Stack<Line>();
    void Update()
    {
        HandleDrawLine();
        HandleUndo();
    }

    private void HandleDrawLine()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_linePrefab, mouseWorldPos, Quaternion.identity, _lineContainer);
            _lineStack.Push(_currentLine);
        }

        if (Input.GetMouseButton(0))
        {
            _currentLine.SetPosition(mouseWorldPos, _drawResolution);
        }
    }

    public void Undo()
    {
        if (_lineStack.Count == 0) return;
        Line line = _lineStack.Pop();
        Destroy(line.gameObject);
    }

    private void HandleUndo()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
    }
}
