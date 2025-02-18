using UnityEngine;

public class Skill_BasicAttack_Template : Skill
{
    [Header("Child class variable")]
    [SerializeField] private SO_Layer _layer;
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _OffsetStandingAttackY;
    [SerializeField] private float _OffsetCrouchingAttackY;

    private PlayerHealthHandler _otherPlayerHealthHandler;
    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    protected override void BeforeSkill()
    { 
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
    }
    protected override void DuringSkill()
    {
        Vector3 start = transform.position;
        Vector3 direction = Vector3.right * transform.localScale.x;

        if (!_inputHandler.isCrouching) 
        {
            start += Vector3.up * _OffsetStandingAttackY;

            if(_inputHandler.isOnGround)
                _inputHandler.CallSkillAnimation(0); // Standing Attack
            else
                _inputHandler.CallSkillAnimation(-2);   // Jumping Attack
        }
        else    
        {
            start += Vector3.down* _OffsetCrouchingAttackY;
            _inputHandler.CallSkillAnimation(-1); // Crouching Attack
        }

        Debug.DrawRay(start, direction * _attackRange, Color.red, 1.5f);   // Display attack ray
        RaycastHit2D[] hitList = Physics2D.RaycastAll(start, Vector2.right, _attackRange, _layer.playerLayer);

        if (hitList.Length > 0)
        {
            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider.gameObject != this.gameObject)
                {
                    if (_otherPlayerHealthHandler == null)
                        _otherPlayerHealthHandler = hit.collider.GetComponent<PlayerHealthHandler>();

                    _otherPlayerHealthHandler.DecreaseHealth(_damageAmount);
                    return;
                }
            }
        }
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}