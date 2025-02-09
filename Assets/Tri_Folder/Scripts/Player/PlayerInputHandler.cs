using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region ~~ Variables ~~
    [SerializeField] private Transform _otherPlayer;
    [SerializeField] private SO_CharacterStat _charStat;
    [SerializeField] private SO_AnimatorHash _animHash;
    [SerializeField] private SO_Layer _layer;
    [SerializeField] private Animator _anim;

    public UnityEvent OnAttackEvent; 
    public UnityEvent OnSkillOneEvent; 
    public UnityEvent OnSkillTwoEvent; 
    public UnityEvent OnSkillThreeEvent;
    public UnityEvent OnDefendEvent;
    public UnityEvent OnCrouchEvent;

    public bool isCanMove;
    public bool isCrouching;
    public bool isDefending;

    private Rigidbody2D _rb;
    private bool _isOnGround;
    private bool _isCanJump;
    private float _moveInput;
    private float _moveSpeed;
    private float _moveDirection;
    #endregion


    #region ~~ Monobehavior handlers ~~
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isOnGround = true;
        _isCanJump = true;
        isCanMove = true;
    }
    private void Start()
    {
        InspectorCheck();
    }
    private void Update()
    {
        //Debug.DrawLine(_transform.position, _transform.position + Vector3.down * _charStat.groundCheckDistance, Color.yellow);    // Display ground check ray
    }
    private void FixedUpdate()
    {
        Helper_FaceOtherPlayer();
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Helper_GroundCheck();
    }
    #endregion


    #region ~~ Action handlers ~~
    private void Move()
    {
        if (_moveInput == 0)
        {
            _rb.linearVelocity = Vector2.up * _rb.linearVelocityY;  
            if (_anim.GetInteger(_animHash.moveDirection) != 0)    
                _anim.SetInteger(_animHash.moveDirection, 0);

            return;
        }

        if (isCanMove)
        {
            _moveDirection = Mathf.Sign(transform.localScale.x * _moveInput);

            if (isCrouching) 
                _moveSpeed = _charStat.moveCrouchingSpeed;
            else
                _moveSpeed = _moveDirection == 1 ? _charStat.moveStandingSpeed : _charStat.moveCrouchingSpeed; // Forward or backward (backward use same speed as crouching)
            
            _rb.linearVelocity = Vector2.right * _moveInput * _moveSpeed + Vector2.up * _rb.linearVelocityY;
            _anim.SetInteger(_animHash.moveDirection, (int)_moveDirection);
        }
    }
    private void Jump()
    {
        if (_isOnGround && _isCanJump)
        {
            _rb.AddForce(Vector2.up * _charStat.jumpForce, ForceMode2D.Impulse);
            _isOnGround = false;
            _isCanJump = false;
            _anim.SetBool(_animHash.isOnGround, _isOnGround);
            _anim.SetTrigger(_animHash.jump);
        }
    }
    #endregion


    #region ~~ Helper Methods ~~
    private void Helper_FaceOtherPlayer()   
    {
        if (transform.localScale.x > 0 && _otherPlayer.position.x < transform.position.x) // is looking Right but other player is on the Left
            transform.localScale = Vector2.left + Vector2.up;
        else if (transform.localScale.x < 0 && _otherPlayer.position.x > transform.position.x)// is looking Left but other player is on the Right
            transform.localScale = Vector3.one;
    }
    private void Helper_GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, _charStat.groundCheckDistance))
        {
            _isOnGround = true;
            _isCanJump = true;
            _anim.SetBool(_animHash.isOnGround, _isOnGround);
        }
    }
    #endregion


    #region ~~ Input handlers ~~
    private void OnMove(InputValue value)
    {
        _moveInput = Mathf.Sign(value.Get<float>());
        isDefending = Mathf.Sign(value.Get<float>()) == -1;
    }
    private void OnJump()
    {
        Jump();
    }
    private void OnCrouch(InputValue value)
    {
        isCrouching = value.Get<float>() == 1;
        _anim.SetBool(_animHash.isCrouching, isCrouching);
    }
    //private void OnDefend(InputValue value)
    //{ 
    //    isDefending = value.Get<float>() == 1;
    //    _anim.SetBool(_animHash.isDefending, isDefending);
    //}
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


    #region ~~ Other handlers ~~
    public void CallSkillAnimation(int skillIndex)
    {
        _anim.SetTrigger(_animHash.useSkill);
        _anim.SetInteger(_animHash.skillIndex, skillIndex);
    }
    private void InspectorCheck()
    {
        if (transform.localScale.x != 1 && transform.localScale.x != -1)
            Debug.LogError("x-axis scale must either be 1 or -1 only");
    }
    #endregion
}