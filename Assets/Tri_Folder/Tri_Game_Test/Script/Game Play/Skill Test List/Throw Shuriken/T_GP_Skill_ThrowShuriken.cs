using UnityEngine;

public class T_GP_Skill_ThrowShuriken : T_GP_Skill  // Skill_ShootProjectile_Template.cs
{
    [Header("Skill exclusive variables")]
    [SerializeField] private Projectile _projectilePrefab;

    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponentInParent<PlayerInputHandler>();
    }

    protected override void BeforeSkill()
    {
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
    }
    protected override void TriggerSkill()
    {
        _projectilePrefab.LaunchProjectile(transform.parent.gameObject);
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}
