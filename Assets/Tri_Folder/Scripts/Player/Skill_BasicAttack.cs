using UnityEngine;

public class Skill_BasicAttack : Skill
{
    [Header("Child class variable")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _OffsetStandingAttackY;
    [SerializeField] private float _OffsetCrouchingAttackY;
    
    private PlayerInputHandler _inputHandler;
    private Transform _transform;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _transform = GetComponent<Transform>();
    }

    protected override void BeforeSkill()
    { 
        _inputHandler.isCanMove = false;
    }
    protected override void DuringSkill()
    {
        Vector3 start = transform.position;
        Vector3 dir = Vector3.right * _transform.localScale.x * _attackRange;

        start += _inputHandler.isCrouching ? Vector3.down * _OffsetCrouchingAttackY : Vector3.up * _OffsetStandingAttackY;
        _inputHandler.CallSkillAnimation(0);

        Debug.DrawRay(start, dir, Color.red, 1.5f);   // Display attack ray
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
    }
}
