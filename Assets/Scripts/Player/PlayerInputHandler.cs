using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInputHandler : MonoBehaviour
{
    #region ~~ Variables ~~
    [Header("Require Components")]
    [SerializeField] private SO_AnimatorHash _animatorHash;
    [SerializeField] private SO_Layer _layer;
    [SerializeField] private SO_CharacterStat _chararacterStat;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _standCollider, _crouchCollider;

    [Header("Unity Events")]
    public UnityEvent<int> OnMoveEvent; // Pass in move direction
    public UnityEvent<bool> OnJumpEvent;    // Pass in is can jump
    public UnityEvent<float> OnVerticalVelocityChangeEvent; // Pass in rb.velocity.y
    public UnityEvent OnLandEvent;
    public UnityEvent OnCrouchEvent;
    public UnityEvent OnBasicAttackEvent;
    public UnityEvent OnSkillOneEvent;
    public UnityEvent OnSkillTwoEvent;
    public UnityEvent OnSkillThreeEvent;
    public UnityEvent OnSkillFourEvent;
    public UnityEvent OnSkillFiveEvent;
    public UnityEvent OnDefendEvent;

    [Header("Public Variables")]
    public Transform otherPlayer;
    public bool isCanMove = true;
    public bool isCanUseSkill = true;
    public bool isCrouching;
    public bool isDefending;
    public bool isOnGround = true;
    public bool isCanJump = true;
    public bool isReverseInput;

    private Rigidbody2D _rb;
    private float _moveInput;
    private float _crouchInput;
    private float _moveSpeed;
    private float _moveDirection;
    #endregion

    #region ~~ Monobehavior handlers ~~
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InspectorCheck();
        _standCollider.enabled = true;
        _crouchCollider.enabled = false;
    }
    private void Update()
    {
        Crouch();
    }
    private void FixedUpdate()
    {
        Helper_FaceOtherPlayer();
        Move();
        if(!isOnGround || _rb.linearVelocityY != 0) OnVerticalVelocityChangeEvent?.Invoke(_rb.linearVelocityY);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Helper_GroundCheck();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _chararacterStat.groundCheckDistance);
    }
    #endregion


    #region ~~ Action handlers ~~
    private void Move()
    {
        if ((!isCanMove || _moveInput == 0) && isCanUseSkill)
        {
            _rb.linearVelocity = Vector2.up * _rb.linearVelocityY;
            isDefending = false;
            OnMoveEvent?.Invoke(0);
        }
        else if (isCanMove)
        {
            _moveDirection = Mathf.Sign(transform.localScale.x * _moveInput * (isReverseInput ? -1 : 1));
            isDefending = _moveDirection == -1;

            if (isCrouching) 
                _moveSpeed = _chararacterStat.moveCrouchingSpeed;
            else
                _moveSpeed = _moveDirection == 1 ? _chararacterStat.moveStandingSpeed : _chararacterStat.moveCrouchingSpeed; // Forward or backward (backward use same speed as crouching)
            
            _rb.linearVelocity = Vector2.right * _moveInput * (isReverseInput ? -1 : 1) * _moveSpeed + Vector2.up * _rb.linearVelocityY;
            OnMoveEvent?.Invoke((int)_moveDirection);
        }
    }
    private void Jump() 
    {
        OnJumpEvent?.Invoke(isCanJump);

        if (isCanMove && isOnGround && isCanUseSkill && isCanJump)
        {
            isOnGround = false;
            isCanJump = false;
            _rb.AddForce(Vector2.up * _chararacterStat.jumpForce, ForceMode2D.Impulse);
        }
    }
    private void Crouch()   
    {
        if (isCanMove && isOnGround && isCanUseSkill && _crouchInput != 0) // Prevent crouching while attacking
        {
            isCrouching = true;
            _animator.SetBool(_animatorHash.isCrouching, true);
            _standCollider.enabled = false;
            _crouchCollider.enabled = true;
        }
        else if (!_standCollider.enabled)
        {
            isCrouching = false;
            _animator.SetBool(_animatorHash.isCrouching, false);
            _standCollider.enabled = true;
            _crouchCollider.enabled = false;
        }
    }
    #endregion


    #region ~~ Helper Methods ~~
    private void Helper_FaceOtherPlayer()   
    {
        if (!isOnGround || !isCanMove || !isCanUseSkill) return;

        if (transform.localScale.x > 0 && otherPlayer.position.x < transform.position.x) // is looking Right but other player is on the Left
            transform.localScale = Vector2.left + Vector2.up;
        else if (transform.localScale.x < 0 && otherPlayer.position.x > transform.position.x)// is looking Left but other player is on the Right
            transform.localScale = Vector3.one;
    }
    private void Helper_GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, _chararacterStat.groundCheckDistance, _layer.groundLayer))
        {
            isOnGround = true;
            isCanJump = true;
            OnLandEvent?.Invoke();
        }
    }
    #endregion


    #region ~~ Input handlers ~~
    private void OnMove(InputValue value)
    {
        _moveInput = Mathf.Ceil(value.Get<float>());
    }
    private void OnJump(InputValue value)
    {
        if (!isReverseInput)
        {
            if(value.Get<float>() == 1)
                Jump();
        }
        else
            _crouchInput = Mathf.Ceil(value.Get<float>());
    }
    private void OnCrouch(InputValue value)
    {
        if (!isReverseInput)
            _crouchInput = Mathf.Ceil(value.Get<float>());
        else
            Jump();
    }
    private void OnAttack(InputValue value)
    {
        if (value.Get<float>() == 1 && isCanUseSkill)
            OnBasicAttackEvent?.Invoke();
    }
    private void OnSkillOne(InputValue value)
    {
        if (value.Get<float>() == 1 && isCanUseSkill)
            OnSkillOneEvent?.Invoke();
    }
    private void OnSkillTwo(InputValue value)
    {
        if (value.Get<float>() == 1 && isCanUseSkill)
            OnSkillTwoEvent?.Invoke();
    }
    private void OnSkillThree(InputValue value)
    {
        if(value.Get<float>() == 1 && isCanUseSkill)
            OnSkillThreeEvent?.Invoke();
    }
    private void OnSkillFour(InputValue value)
    {
        if (value.Get<float>() == 1 && isCanUseSkill)
            OnSkillFourEvent?.Invoke();
    }
    private void OnSkillFive(InputValue value)
    {
        if (value.Get<float>() == 1 && isCanUseSkill)
            OnSkillFiveEvent?.Invoke();
    }
    #endregion


    #region ~~ Other handlers ~~
    private void InspectorCheck()
    {
        if (transform.localScale.x != 1 && transform.localScale.x != -1)
            Debug.LogError("x-axis scale must either be 1 or -1 only", gameObject);
    }
    #endregion

    #region ~~ Special effects ~~
    public void Public_ReverseMovementInput(float duration)
    {
        StartCoroutine(ReverseInputOverTimeCoroutine(duration));
    }
    private IEnumerator ReverseInputOverTimeCoroutine(float duration)
    {
        isReverseInput = true;
        yield return new WaitForSeconds(duration);
        isReverseInput = false;
    }
    #endregion
}