using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar_Manager : MonoBehaviour
{
    [SerializeField] private Image _p1Fill, _p2Fill;
    private float p1MaxHealth, p2MaxHealth;

    public void Public_SetUp(GameObject p1, GameObject p2)
    {
        PlayerHealthHandler p1Health = p1.GetComponent<PlayerHealthHandler>();
        PlayerHealthHandler p2Health = p2.GetComponent<PlayerHealthHandler>();
        p1MaxHealth = p1Health.health;
        p2MaxHealth = p2Health.health;

        p1Health.OnHealthIncreaseEvent.AddListener(SetPlayerOneHealth);
        p1Health.OnHealthDecreaseEvent.AddListener(SetPlayerOneHealth);

        p2Health.OnHealthIncreaseEvent.AddListener(SetPlayerTwoHealth);
        p2Health.OnHealthDecreaseEvent.AddListener(SetPlayerTwoHealth);

        SetPlayerOneHealth(p1MaxHealth);
        SetPlayerTwoHealth(p2MaxHealth);
    }
    public void SetPlayerOneHealth(float health) 
    {
        Debug.Log("Loose health p1 " + health / p1MaxHealth);
        _p1Fill.fillAmount = health / p1MaxHealth;
    }
    public void SetPlayerTwoHealth(float health) 
    {
        Debug.Log("Loose health p2 " + health / p2MaxHealth);
        _p2Fill.fillAmount = health / p2MaxHealth;
    }
}
