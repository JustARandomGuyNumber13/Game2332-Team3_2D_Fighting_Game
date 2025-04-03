using System.Collections;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] GameObject trapPrefab;

    private void Start()
    {
        //MyEvent.AddListener(StartTrap);
    }

    public void PoisoningCloud()
    {
        Instantiate(trapPrefab, new Vector3(Random.Range(-8, 8), 0), Quaternion.identity);
    }

    IEnumerator CloudSpawner()
    {
        PoisoningCloud();
        yield return new WaitForSeconds(5);
        PoisoningCloud();
        yield return new WaitForSeconds(5);
        PoisoningCloud();
        yield return new WaitForSeconds(5);  
        
        //could do while statement to continue spawning clouds until the condition is not met
        //for now it's just 3 clouds
    }

    public void StartTrap()
    {
        StartCoroutine(CloudSpawner());
    }

}
