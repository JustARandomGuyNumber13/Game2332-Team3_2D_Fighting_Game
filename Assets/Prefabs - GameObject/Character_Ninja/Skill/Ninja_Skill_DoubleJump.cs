using UnityEngine;
using UnityEngine.Events;

public class Ninja_Skill_DoubleJump : Skill // Skill_Passive_Template.cs
{
    [Header("Skill exclusive variables")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private UnityEvent OnDoubleJumpEvent;

    private bool _isCanDoubleJump = true;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void TriggerSkill()
    {
        if(!_inputHandler.isOnGround && _inputHandler.isCanUseSkill && _isCanDoubleJump)
        {
            OnDoubleJumpEvent?.Invoke();
            _rb.linearVelocity = Vector2.right * _rb.linearVelocityX;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isCanDoubleJump = false;
        }
        else if (_inputHandler.isOnGround && !_isCanDoubleJump)
        {
            _isCanDoubleJump = true;
        }
    }
}
