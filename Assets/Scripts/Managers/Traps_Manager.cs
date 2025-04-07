using System.Collections;
using UnityEngine;

public class Traps_Manager : MonoBehaviour
{
    [SerializeField] private Trap[] trapList;
    [SerializeField] private Trap deathWall;

    [SerializeField] private float startSpawnRate;
    [SerializeField] private float decreaseRate;
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float rateDecreaseInterval;
    private float curSpawnRate;

    public void Public_StartGame()
    {
        StartCoroutine(PhaseChangeCoroutine());
    }

    private IEnumerator PhaseChangeCoroutine()
    {
        curSpawnRate = startSpawnRate;
        yield return new WaitForSeconds(60);
        if(Game_Manager.IsEndGame) yield break;
        StartCoroutine(SpawnTrapCoroutine());
        StartCoroutine(IncreaseSpawnRateCoroutine());

        yield return new WaitForSeconds(60);
        if (Game_Manager.IsEndGame) yield break;
        SpawnDeathWall();

        yield return new WaitForSeconds(30);
        if (Game_Manager.IsEndGame) yield break;
        ActivateDeathWall();
    }
    private IEnumerator SpawnTrapCoroutine()
    {
        while (!Game_Manager.IsEndGame)
        {
            GetRandomTrap().Activate();
            yield return new WaitForSeconds(curSpawnRate);
        }
    }
    private IEnumerator IncreaseSpawnRateCoroutine()
    { 
        while(!Game_Manager.IsEndGame)
        {
            yield return new WaitForSeconds(rateDecreaseInterval);
            if (curSpawnRate > minSpawnRate) curSpawnRate -= decreaseRate;
        }
    }

    private Trap GetRandomTrap()
    {
        int randIndex = Random.Range(0, trapList.Length);

        while (!trapList[randIndex].IsAvailable)
        {
            randIndex++;
            if (randIndex == trapList.Length)
                randIndex = 0;
        }

        return trapList[randIndex];
    }
    private void SpawnDeathWall()
    { 
        deathWall.gameObject.SetActive(true);
    }
    private void ActivateDeathWall()
    { 
        deathWall.Activate();
    }
}
