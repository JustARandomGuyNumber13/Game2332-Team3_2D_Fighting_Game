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
    public UnityEvent OnDefendEvent;

    private PlayerInputHandler _inputHandler;
    private float _health;
    //public float health
    //{ get; private set; }

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }
    private void Start()
    {
        _health = _characterStat.maxHealth;
    }

    public void Public_IncreaseHealth(float amount)
    {
        _health += amount;
        OnHealthIncreaseEvent?.Invoke(_health);
    }
    public void Public_DecreaseHealth(float amount)
    {
        float damageAmount = (amount - _characterStat.defenseValue);
        Debug.Log(gameObject.name + " decrease _health");

        if (_inputHandler.isDefending)
        {
            OnDefendEvent?.Invoke();
            damageAmount *= 0.2f; // This float is adjustable to match balance (Take 20% of damage if is defending)
        }

        _health -= damageAmount;
        OnHealthDecreaseEvent?.Invoke(_health);
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
                _health -= amount;
                OnHealthDecreaseOverTimerEvent?.Invoke(_health);
            }
            yield return null;
        }
    }
}
