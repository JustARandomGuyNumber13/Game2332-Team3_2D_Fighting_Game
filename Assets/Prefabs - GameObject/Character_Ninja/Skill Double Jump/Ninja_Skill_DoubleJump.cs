using UnityEngine;

public class Ninja_Skill_DoubleJump : Skill
{
    [SerializeField] private float _jumpForce;
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
            Debug.Log("Ninja Double Jump", gameObject);
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
