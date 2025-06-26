using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider;
    private List<Vector2> _allPoints = new List<Vector2>();
    private float _lineLength;
    public float LineLength => _lineLength;
    private float _lengthToAdd;

    public int PositionCount
    {
        get { return _lineRenderer.positionCount; }
    }
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _edgeCollider = GetComponent<EdgeCollider2D>();
    }

    public void SetPosition(Vector2 position, float drawResolution)
    {
        if (!CanAppend(position, drawResolution)) return;
        if (_lineRenderer.positionCount != 0)
        {
            _lengthToAdd = Vector3.Distance(position, _lineRenderer.GetPosition(_lineRenderer.positionCount - 1));
            _lineLength += _lengthToAdd;
            DrawManager.Instance.UpdateTotalLength(_lengthToAdd);
        }
        ++_lineRenderer.positionCount;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        _allPoints.Add(transform.InverseTransformPoint(position));
        _edgeCollider.points = _allPoints.ToArray();

    }

    private bool CanAppend(Vector2 position, float drawResolution)
    {
        if (_lineRenderer.positionCount == 0) return true;
        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position) > drawResolution;
    }
}
