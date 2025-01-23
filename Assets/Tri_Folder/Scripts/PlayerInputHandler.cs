using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region ~~ Variables ~~
    [SerializeField] private Transform _otherPlayer;
    [SerializeField] private SO_CharacterStat _charStat;
    [SerializeField] private SO_Layer _layer;

    public UnityEvent OnAttackEvent; 
    public UnityEvent OnSkillOneEvent; 
    public UnityEvent OnSkillTwoEvent; 
    public UnityEvent OnSkillThreeEvent;
    public UnityEvent OnDefendEvent;
    public UnityEvent OnCrouchEvent;

    private Transform _transform;
    private Rigidbody2D _rb;
    private bool _isCanMove;
    private bool _isOnGround;
    private bool _isCanJump;
    private bool _isCrouching;
    private bool _isDefending;
    private float _moveInput;
    private float _moveSpeed;
    #endregion

    #region ~~ Monobehavior handlers ~~
    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _isOnGround = true;
        _isCanJump = true;
        _isCanMove = true;
    }
    private void Start()
    {
        InspectorCheck();
    }
    private void Update()
    {
        Debug.DrawLine(_transform.position, _transform.position + Vector3.down * _charStat.groundCheckDistance, Color.yellow);
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(_transform.position, Vector2.down, _charStat.groundCheckDistance))
        {
            _isOnGround = true;
            _isCanJump = true;
        }
    }
    #endregion


    #region ~~ Action handlers ~~
    private void Move()
    {
        Helper_FaceOtherPlayer();
        if (_isCanMove)
        {
            _moveSpeed = _moveInput - _transform.localScale.x == 0 ? _charStat.moveStandingSpeed : _charStat.moveCrouchingSpeed;
            if (_isCrouching) _moveSpeed = _charStat.moveCrouchingSpeed;
            if (_isDefending) _moveSpeed = 0;
            _rb.linearVelocity =  Vector2.right *_moveInput * _moveSpeed + Vector2.up * _rb.linearVelocityY; 
        }
    }
    private void Jump()
    {
        if (_isOnGround && _isCanJump)
        {
            _rb.AddForce(Vector2.up * _charStat.jumpForce, ForceMode2D.Impulse);
            _isOnGround = false;
            _isCanJump = false;
        }
    }
    #endregion


    #region ~~ Helper Methods ~~
    private void Helper_FaceOtherPlayer()   
    {
        if (_transform.localScale.x > 0 && _otherPlayer.position.x < _transform.position.x) // is looking Right but other player is on the Left
            _transform.localScale = Vector2.left + Vector2.up;
        else if (_transform.localScale.x < 0 && _otherPlayer.position.x > _transform.position.x)// is looking Left but other player is on the Right
            _transform.localScale = Vector3.one;
    }
    #endregion
    

    #region ~~ Input handlers ~~
    private void OnMove(InputValue value)
    {
        _moveInput = Mathf.Ceil(value.Get<float>());// -1, 0, 1
    }
    private void OnJump()
    {
        Jump();
    }
    private void OnCrouch(InputValue value)
    {
        _isCrouching = value.Get<float>() == 1;
    }
    private void OnDefend(InputValue value)
    { 
        _isDefending = value.Get<float>() == 1;
    }
    private void OnAttack()
    {
        OnAttackEvent?.Invoke();
    }
    private void OnSkillOne()
    {
        OnSkillOneEvent?.Invoke();
    }
    private void OnSkillTwo()
    {
        OnSkillTwoEvent?.Invoke();
    }
    private void OnSkillThree()
    {
        OnSkillThreeEvent?.Invoke();
    }
    #endregion

    private void InspectorCheck()
    {
        if (_transform.localScale.y != 1 && _transform.localScale.y != -1)
            Debug.LogError("x-axis scale must either be 1 or -1 only");
    }
}
