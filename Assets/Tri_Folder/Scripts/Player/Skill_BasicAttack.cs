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
    
    }
    protected override void DuringSkill()
    {
        Vector3 start = transform.position;
        Vector3 dir = Vector3.right * _transform.localScale.x * _attackRange;

        if (_inputHandler.isCrouching)
        {
            start += Vector3.down * _OffsetCrouchingAttackY;
            dir += Vector3.down * _OffsetCrouchingAttackY;
        }
        else
        {
            start += Vector3.up * _OffsetStandingAttackY;
            dir += Vector3.up * _OffsetStandingAttackY;
        }

        Debug.DrawRay(start, dir, Color.red, 3f);
    }
    protected override void AfterSkill() 
    {
    
    }
}
