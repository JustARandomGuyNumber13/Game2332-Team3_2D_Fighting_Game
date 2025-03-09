using UnityEngine;

public class Ninja_Skill_ShootProjectile_ThrowShuriken_Test : Skill_Test  // Skill_ShootProjectile_Template.cs
{
    [Header("Skill exclusive variables")]
    [SerializeField] private Projectile _projectilePrefab;

    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = transform.GetComponentInParent<PlayerInputHandler>();
    }

    protected override void BeforeSkill()
    {
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
    }
    protected override void TriggerSkill()
    {
        Debug.Log("Ninja Throw Shuriken", gameObject);
        _projectilePrefab.LaunchProjectile(transform.parent.gameObject);
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}
