using System.Collections;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private SO_CharacterStat _characterStat;
    [SerializeField] private HealthBar healthBar; //Reference to health bar script

    private PlayerInputHandler _inputHandler;

    public float health
    { get; private set; }

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }
    private void Start()
    {
        health = _characterStat.maxHealth;
        healthBar.SetHealth(health, _characterStat.maxHealth); //Calls SetHealth function
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        healthBar.SetHealth(health, _characterStat.maxHealth); //SetHealth
    }
    public void DecreaseHealth(float amount)
    {
        float damageAmount = (amount - _characterStat.defenseValue);

        if (_inputHandler.isDefending)
        {
            damageAmount *= 0.2f; // This float is adjustable to match balance (Take 20% of damage if is defending)
            _inputHandler.CallDefendAnimation();
        }
        else
        {
            _inputHandler.CallHurtAnimation();
        }

        health -= damageAmount;
        healthBar.SetHealth(health, _characterStat.maxHealth); //SetHealth
    }

    public void DecreaseHealthOverTime(float amount, float duration, float tickDuration)
    { 
        StartCoroutine(DecreaseHealthOverTimeCoroutine(amount, duration, tickDuration));
    }
    private IEnumerator DecreaseHealthOverTimeCoroutine(float amount, float duration, float tickDuration)
    {
        float timer = 0;
        float tick = tickDuration;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (timer >= tick)
            {
                tick += tickDuration;
                DecreaseHealth(amount);
            }
            yield return null;
        }
    }
}
