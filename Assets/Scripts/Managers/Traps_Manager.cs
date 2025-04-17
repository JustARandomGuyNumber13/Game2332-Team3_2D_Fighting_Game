using System.Collections;
using UnityEngine;

public class Traps_Manager : MonoBehaviour
{
    [SerializeField] private float phaseOneDuration;
    [SerializeField] private float phaseTwoDuration;
    [SerializeField] private float phaseThreeDuration;

    [SerializeField] private Trap[] trapList;
    [SerializeField] private Trap deathWall;

    [SerializeField] private float spawnRate;
    private int curIndex;

    //[SerializeField] private float startSpawnRate;
    //[SerializeField] private float decreaseRate;
    //[SerializeField] private float rateDecreaseInterval;
    //[SerializeField] private int decreaseCount;
    //private float curSpawnRate;

    public void Public_StartGame()
    {
        StartCoroutine(PhaseChangeCoroutine());
    }

    private IEnumerator PhaseChangeCoroutine()
    {
        //curSpawnRate = startSpawnRate;
        yield return new WaitForSeconds(phaseOneDuration);
        if(Game_Manager.IsEndGame) yield break;
        StartCoroutine(SpawnTrapCoroutine());
        //StartCoroutine(IncreaseSpawnRateCoroutine());

        yield return new WaitForSeconds(phaseTwoDuration);
        if (Game_Manager.IsEndGame) yield break;
        SpawnDeathWall();

        yield return new WaitForSeconds(phaseThreeDuration);
        if (Game_Manager.IsEndGame) yield break;
        ActivateDeathWall();
    }
    private IEnumerator SpawnTrapCoroutine()
    {
        while (!Game_Manager.IsEndGame)
        {
            GetRandomTrap().Activate();
            yield return new WaitForSeconds(spawnRate);
        }
    }
    //private IEnumerator IncreaseSpawnRateCoroutine()
    //{ 
    //    while(!Game_Manager.IsEndGame)
    //    {
    //        yield return new WaitForSeconds(rateDecreaseInterval);
    //        if (decreaseCount > 0) curSpawnRate -= decreaseRate;
    //    }
    //}

    //private Trap GetRandomTrap()
    //{
    //    int randIndex = Random.Range(0, trapList.Length - 1);

    //    while (!trapList[randIndex].IsAvailable)
    //    {
    //        randIndex--;
    //        //if (randIndex == trapList.Length)
    //        //    randIndex = 0;

    //        if (randIndex == -1)
    //            randIndex = trapList.Length - 1;
    //    }

    //    return trapList[randIndex];
    //}

    private Trap GetRandomTrap()
    {
        Trap trap = trapList[curIndex];
        curIndex++;

        if (curIndex >= trapList.Length)
            curIndex = 0;

        return trap;
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
