using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private SO_CharacterStat _characterStat;

    public UnityEvent<float> OnHealthIncreaseEvent;
    public UnityEvent<float> OnHealthIncreaseOverTimerEvent;
    public UnityEvent<float> OnHealthDecreaseEvent;
    public UnityEvent<float> OnHealthDecreaseOverTimerEvent;
    public UnityEvent OnDeathEvent;
    public UnityEvent OnDefendEvent;

    private PlayerInputHandler _inputHandler;
    public bool IsDead { get; private set; }
    public float health
    { get; private set; }

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        health = _characterStat.maxHealth;  // Either be in Awake or OnEnable
    }

    public void Public_IncreaseHealth(float amount)
    {
        health += amount;
        OnHealthIncreaseEvent?.Invoke(health);
    }
    public void Public_DecreaseHealth(float amount)
    {
        float damageAmount = (amount - _characterStat.defenseValue);
        Debug.Log(gameObject.name + " decrease health");

        if (_inputHandler.isDefending)
        {
            OnDefendEvent?.Invoke();
            damageAmount *= 0.2f; // This float is adjustable to match balance (Take 20% of damage if is defending)
        }

        health -= damageAmount;
        OnHealthDecreaseEvent?.Invoke(health);
        DeathCheck();
    }

    public void Public_DecreaseHealthOverTime(float amount, float duration, float tickDuration)
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
                OnHealthDecreaseOverTimerEvent?.Invoke(health);
                DeathCheck();
            }
            yield return null;
        }
    }

    private void DeathCheck()
    {
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " die!", gameObject);
            IsDead = true;
            OnDeathEvent?.Invoke();
        }
    }
}
