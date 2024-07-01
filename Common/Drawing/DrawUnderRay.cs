using UnityEngine;

public class DrawUnderRay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color _rayColor;
    [SerializeField] private Color _secondColor;
    [SerializeField] private float _startDraw;
    [SerializeField] private float _endDraw;

    private Vector3 _startVector = new Vector3(0f, 1f, 0f);
    private Vector3 _endVector = new Vector3(0f, 1f, 0f);

    private Transform _selfTransform;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 down = _selfTransform.TransformDirection(Vector3.down) * _endDraw;
        Vector3 start = _selfTransform.position;

        start.y *= _startDraw;

        Debug.DrawRay(start, down, _rayColor); 
    }
}
