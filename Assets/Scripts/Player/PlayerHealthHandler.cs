using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private SO_CharacterStat _characterStat;
    public UnityEvent OnHealthDecrease;
    public UnityEvent OnHealthDecreaseOverTimer;
    public UnityEvent OnHealthIncrease;
    public UnityEvent OnHealthIncreaseOverTimer;

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
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        OnHealthIncrease?.Invoke();
    }
    public void DecreaseHealth(float amount)
    {
        float damageAmount = (amount - _characterStat.defenseValue);
        Debug.Log(gameObject.name + " decrease health");

        if (_inputHandler.isDefending)
            damageAmount *= 0.2f; // This float is adjustable to match balance (Take 20% of damage if is defending)

        health -= damageAmount;
        OnHealthDecrease?.Invoke();
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
                health -= amount;
                OnHealthDecreaseOverTimer?.Invoke();
            }
            yield return null;
        }
    }
}
