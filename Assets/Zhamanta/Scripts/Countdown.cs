using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    Text countdownText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }

  

    public  IEnumerator runCountdown(float seconds)
    {
        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            theCountdowns(seconds);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void theCountdowns(float seconds)
    {
        
        seconds -= 1;
        countdownText.text = seconds.ToString("0");
        Debug.Log(seconds);
       
        

        if (seconds <= 0)
        {
            seconds = 0;
        }
                
    }
}
