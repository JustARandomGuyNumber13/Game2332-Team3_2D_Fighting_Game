using UnityEngine;
using System.Collections;

public class Spawntest : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int amount = 5; // Amount of meteors that can be spawned
    [SerializeField] private Vector2 spawnZoneMin, spawnZoneMax;
    [SerializeField] private float spawnDelay = 1f;

    private void Start()
    {
        StartCoroutine(SpawnMeteorsOneByOne());
    }
    private IEnumerator SpawnMeteorsOneByOne()
    {
        for (int i = 0; i < amount; i++) // Loop to spawn the desired amount of meteors
        {
            // Instantiate meteor and assign spawn zone
            GameObject trap = Instantiate(prefab);
            MeteorTrap meteorTrap = trap.GetComponent<MeteorTrap>();
            meteorTrap.InitializeSpawnZone(spawnZoneMin, spawnZoneMax);
            meteorTrap.Activate();

            yield return new WaitForSeconds(spawnDelay); // Wait before spawning the next meteor
        }
    }

}
