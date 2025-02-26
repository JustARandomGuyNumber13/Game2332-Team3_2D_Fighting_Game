using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    private float maxHealth;

    public void Public_SetUp(PlayerHealthHandler handler)
    { 
        maxHealth = handler.health;
        handler.OnHealthIncreaseEvent.AddListener(SetHealth);
        handler.OnHealthDecreaseEvent.AddListener(SetHealth);
        SetHealth(maxHealth);
    }
    public void SetHealth(float health) // Call by Unity Event
    {
        fillImage.fillAmount = health / maxHealth;
    }
}
