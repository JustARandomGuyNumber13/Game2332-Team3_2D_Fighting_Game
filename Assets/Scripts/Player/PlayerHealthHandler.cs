using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private SO_CharacterStat _characterStat;

    private PlayerInputHandler _inputHandler;

    private float _health;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }
    private void Start()
    {
        _health = _characterStat.maxHealth;
    }

    public void IncreaseHealth(float amount)
    {
        _health += amount;
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

        _health -= damageAmount;
    }
}
