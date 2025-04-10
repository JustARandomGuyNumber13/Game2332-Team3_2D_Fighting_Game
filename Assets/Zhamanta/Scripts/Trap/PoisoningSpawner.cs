using System.Collections;
using UnityEngine;

//Poisoning spawn manager

public class PoisoningSpawner : TrapParent
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Poisoning[] clouds;
    [SerializeField] float cooldown;

 /*   Vector3 minOffsetX;
    Vector3 maxOffsetX;*/
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

    /*private void OnDrawGizmos()
    {
        minOffsetX = camPos + spawnZoneMin.x * Vector3.right * camSize;
        maxOffsetX = camPos + spawnZoneMax.x * Vector3.right * camSize;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(camPos, minOffsetX);
        Gizmos.DrawLine(camPos, maxOffsetX);
    }*/

    private Vector3 SpawnRandomPoint()
    {

        float randomX = Random.Range(spawnZoneMin.x * camSize, spawnZoneMax.x * camSize);
        float randomY = Random.Range(0, spawnZoneMax.y);

        return new Vector3(randomX, randomY, 0);
    }

    IEnumerator Cooldown()
    {
        int currentIndex = 0;

        while (currentIndex < clouds.Length)
        {
            clouds[currentIndex].Activate();
            clouds[currentIndex].transform.position = SpawnRandomPoint();
            currentIndex++;
            yield return new WaitForSeconds(cooldown);
        }

        Deactivate(); 
    }
}
