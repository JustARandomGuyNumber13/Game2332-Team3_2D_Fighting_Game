using UnityEngine;

public class Skill_Dash_Template : Skill
{
    [SerializeField] private float _dashSpeed;

    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void BeforeSkill()
    {
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
    }
    protected override void DuringSkill(float timer)
    {
        _rb.linearVelocity = Vector2.up * _rb.linearVelocityY + Vector2.right * transform.localScale.x * _dashSpeed;
        Debug.Log("Dashing: " + _rb.linearVelocity, gameObject);
    }
    protected override void AfterSkill()
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}
