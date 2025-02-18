using System.Collections;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private SO_CharacterStat _characterStat;

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

    public void ReverseMovementInput(float duration)
    {
        StartCoroutine(ReverseInputOverTimeCoroutine(duration));
    }
    private IEnumerator ReverseInputOverTimeCoroutine(float duration)
    {
        float timer = 0;
        _inputHandler.isReverseInput = true;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _inputHandler.isReverseInput = false;
    }
}
