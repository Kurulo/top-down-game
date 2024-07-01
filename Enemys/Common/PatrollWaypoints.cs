using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PatrollWaypoints : MonoBehaviour {
    [SerializeField] private List<Vector3> _path = new List<Vector3>();
    private List<Vector3> _adjustedWaypoints = new List<Vector3>();

    [SerializeField] private Vector3 _startPos;
    [SerializeField, Range(0f, 100f)] private float _thicknessOfLine;

    private bool _drawPath = false;
    public void SetDrawPath() => _drawPath = !_drawPath;

    private void Start() {
        _startPos = transform.position;
        _drawPath = false;  

        ConstructAdjustedWay();
    }

    private void Update() {
        if (_drawPath) {
            ConstructAdjustedWay();
        }
    }

    private void ConstructAdjustedWay() {
        _adjustedWaypoints.Clear();
        for (int i = 0; i < _path.Count; i++) {
            Vector3 adjustedVector = AdjustVector(_path[i]);
            _adjustedWaypoints.Add(adjustedVector);
        }
    }

    public List<Vector3> GetPath() {
        ConstructAdjustedWay();
        return _adjustedWaypoints;
    }

    private void OnDrawGizmos() {
        if (_adjustedWaypoints.Count == 0) return;
        if (_startPos == null) _startPos = transform.position;

        for (int i = 0; i < _path.Count; i++) {
            
            if (i == 0) {
                Handles.color = Color.green;
                Handles.DrawSolidDisc(_adjustedWaypoints[i], Vector3.up, 1f);
                continue;
            }

            Handles.color = Color.white;
            Handles.DrawLine(_adjustedWaypoints[i - 1], _adjustedWaypoints[i], _thicknessOfLine);
            Handles.color = Color.red;
            Handles.DrawSolidDisc(_adjustedWaypoints[i], Vector3.up, 1f);
        }    
    }

    private Vector3 AdjustVector(Vector3 vector) => _startPos + vector;

    public void ResetStartPosition() => _startPos = transform.position;
}
