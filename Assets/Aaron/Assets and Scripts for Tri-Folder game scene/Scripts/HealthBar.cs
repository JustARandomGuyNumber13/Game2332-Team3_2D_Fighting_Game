using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private PlayerHealthHandler handler;

    private float maxHealth;

    public void Start()
    {
        maxHealth = handler._characterStat.maxHealth;
        handler.OnHealthIncreaseEvent.AddListener(SetHealth);
        handler.OnHealthDecreaseEvent.AddListener(SetHealth);
        SetHealth(maxHealth);
    }
    public void SetHealth(float health)
    {
        fillImage.fillAmount = health / maxHealth;
    }
}
