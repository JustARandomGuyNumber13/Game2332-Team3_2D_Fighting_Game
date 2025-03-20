using UnityEngine;
using UnityEngine.Windows;

public class T_GP_Skill_DoubleJump : T_GP_Skill // Skill_Passive_Template.cs
{
    [Header("Skill exclusive variables")]
    [SerializeField] private float _jumpForce;

    private bool _isCanDoubleJump = true;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponentInParent<PlayerInputHandler>();
        _rb = GetComponentInParent<Rigidbody2D>();

        _inputHandler.OnJumpEvent.AddListener(TriggerSkill);
        _inputHandler.OnLandEvent.AddListener(TriggerSkill);
    }
    private void TriggerSkill(bool value)
    { TriggerSkill(); }
    protected override void TriggerSkill()
    {
        if(!_inputHandler.isOnGround && _inputHandler.isCanUseSkill && _isCanDoubleJump)
        {
            Debug.Log("Ninja Double Jump");
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
