using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    private float maxHealth;

    public void Public_SetUp(GameObject healthHandler)
    {
        PlayerHealthHandler health = healthHandler.GetComponent<PlayerHealthHandler>();
        maxHealth = health.health;
        health.OnHealthIncreaseEvent.AddListener(SetHealth);
        health.OnHealthDecreaseEvent.AddListener(SetHealth);
        SetHealth(maxHealth);
    }
    public void SetHealth(float health) // Call by Unity Event
    {
        fillImage.fillAmount = health / maxHealth;
    }
}
