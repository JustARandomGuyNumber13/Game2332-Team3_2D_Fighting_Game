using UnityEngine;
using UnityEngine.UIElements;

public class Camera_Manager : MonoBehaviour
{
    [Header("Boundary points")]
    [SerializeField] private float _right;
    [SerializeField] private float _left;
    [SerializeField] private float _top;
    [SerializeField] private float _bottom;

    [Header("Camera size")]
    [SerializeField] private float _minCamSize;
    [SerializeField] private float _maxCamSize;
    [SerializeField] private Vector2 _playerOffset;

    private Transform _player1;
    private Transform _player2;
    private Camera _camera;
    private float _camSize;    
    private Vector3 _camPos;
    private Vector3 _playerMidPoint;
    private Vector2 _playerDistance;
    private float _leftBound, _rightBound;
    private float _hScale, _vScale;


    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    private void Start()
    {
        _camPos = transform.position;   // Require for default Z-axis setup
    }
    private void LateUpdate()
    {
        _playerMidPoint = (_player1.position + _player2.position) / 2;
        _playerDistance.x = Mathf.Abs(_player1.position.x - _player2.position.x);
        _playerDistance.y = Mathf.Abs(_player1.position.y - _player2.position.y);
        UpdateCameraSize();
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        _leftBound = _left + _camera.orthographicSize * _camera.aspect;
        _rightBound = _right - _camera.orthographicSize * _camera.aspect;

        _camPos.x = Mathf.Clamp(_playerMidPoint.x, _leftBound, _rightBound);       // Horizontal: Follow _midPoint.x while keep inside horizontal boundaries
        _camPos.y = _bottom + _camSize;                                                                  // Vertical: Anchor view to bottom, no top boundary use currently
        transform.position = _camPos;
    }
    private void UpdateCameraSize()
    {
        _hScale = (_playerDistance.x + _playerOffset.x * 2) / (_camera.aspect * 2);    // Horizontal: Scale with _playerDistance (Distance between 2 players)
        _vScale = (_playerDistance.y + _playerOffset.y * 2) / (_camera.aspect * 2);         // Vertical: Scale if player jump while at small size

        _camSize = Mathf.Max(_hScale, _vScale);
        _camSize = Mathf.Clamp(_camSize, _minCamSize, _maxCamSize);
        _camera.orthographicSize = _camSize;
    }

    public void Public_SetUp(Transform p1, Transform p2)
    { 
        _player1 = p1;
        _player2 = p2;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 topLeft = Vector3.right * _left + Vector3.up * _top;
        Vector3 topRight = Vector3.right * _right + Vector3.up * _top;
        Vector3 botLeft = Vector3.right * _left + Vector3.up * _bottom;
        Vector3 botRight = Vector3.right * _right + Vector3.up * _bottom;

        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botLeft, topLeft);
        Gizmos.DrawLine(botRight, topRight);
    }
}
