using UnityEngine;

public class DeathWallSpawner : Trap
{
    [SerializeField] Camera mainCamera;
    [SerializeField] private DeathWallTrap[] walls;

    Vector3 camPos;
    float camSize;

    private void Start()
    {
        camPos = mainCamera.transform.position;
        camSize = mainCamera.orthographicSize * mainCamera.aspect;
    }
}
