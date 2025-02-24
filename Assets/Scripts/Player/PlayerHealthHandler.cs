using System.Collections;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] public SO_CharacterStat _characterStat; //Changed from private to public
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

    public void SetHealth(float amount) //created for the round timer script to reference
    {
        health = Mathf.Clamp(amount, 0, _characterStat.maxHealth); //Ensuring that health is within an acceptable range to avoid a number error
        healthBar.SetHealth(health, _characterStat.maxHealth);
    }
    public void IncreaseHealth(float amount)
    {
        //health += amount;
        //healthBar.SetHealth(health, _characterStat.maxHealth); //SetHealth
        SetHealth(health + amount); //using SetHealth method to ensure health number is valid
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
