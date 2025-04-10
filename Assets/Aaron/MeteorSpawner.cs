using System.Collections;
using UnityEngine;

public class MeteorSpawner : Trap
{
    [SerializeField] Camera mainCamera;
    [SerializeField] private MeteorTrap[] meteors;
    [SerializeField] private float cooldown;
    [SerializeField] private float deactivateDelay;

    Vector3 camPos;
    float camSize;

    private void Start()
    {
        camPos = mainCamera.transform.position;
        camSize = mainCamera.orthographicSize * mainCamera.aspect;
        StartCoroutine(Cooldown());
    }
    protected override void TrapBehavior()
    {
        StartCoroutine(Cooldown());
    }

    private Vector3 SpawnRandomPoint()
    {
        float randomX = Random.Range(spawnZoneMin.x * camSize, spawnZoneMax.x * camSize);
        float spawnPosY = spawnZoneMax.y * camSize; 
        return new Vector3(randomX, spawnPosY, 0);
    }

    IEnumerator Cooldown()
    {
        int currentIndex = 0;

        while (currentIndex < meteors.Length)
        {
            meteors[currentIndex].Activate();
            meteors[currentIndex].transform.position = SpawnRandomPoint();
            currentIndex++;
            yield return new WaitForSeconds(cooldown);
        }

        Invoke("Deactivate", deactivateDelay); 
    }
}
