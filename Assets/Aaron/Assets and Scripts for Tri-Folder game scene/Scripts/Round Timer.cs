using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private float startingTime = 180f; //3 minutes as a starting point. Can be adjustable
    [SerializeField] private TextMeshProUGUI timertext;
    [SerializeField] private PlayerHealthHandler[] healthHandler;

    private float currentTime;
    private bool isResetting = false;

    private void Start()
    {
        currentTime = startingTime;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }

        StartCoroutine(ResetHealthAndTimer());
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator ResetHealthAndTimer()
    {
        isResetting = true;
        float duration = 5f; //Duration for the health and timer to reset

        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            foreach (var healthHandle in healthHandler)
            {
                float initialHealth = healthHandle.health;
                float targethealth = healthHandle._characterStat.maxHealth;
                healthHandle.SetHealth(Mathf.Lerp(initialHealth, targethealth, timer / duration));
            }
            
            currentTime = Mathf.Lerp(0, startingTime, timer / duration);
            UpdateTimerDisplay();
            yield return null;
        }

        foreach (var healthHandle in healthHandler)
        {
            healthHandle.SetHealth(healthHandle._characterStat.maxHealth);
        }
        
        currentTime = startingTime;
        UpdateTimerDisplay();
        isResetting = false;
        StartCoroutine(UpdateTimer());
    }
}
