using UnityEngine;

public class Background_Parallax_Effect : MonoBehaviour
{
    [SerializeField] private float _parallaxEffectMultiplierX = 0.5f; // Only X-axis multiplier

    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;
    private float _initialYPosition; // Store the initial Y position

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        _initialYPosition = transform.position.y; // Store the initial Y
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        float newXPosition = transform.position.x + deltaMovement.x * _parallaxEffectMultiplierX;

        transform.position = new Vector3(newXPosition, _initialYPosition, transform.position.z); // Keep Y fixed
        _lastCameraPosition = _cameraTransform.position;
    }
}