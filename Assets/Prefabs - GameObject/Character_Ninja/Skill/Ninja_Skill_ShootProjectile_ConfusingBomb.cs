using UnityEngine;

public class Ninja_Skill_ShootProjectile_ConfusingBomb : Skill  // Skill_ShootProjectile_Template.cs
{
    [Header("Skill exclusive variables")]
    [SerializeField] private Projectile _projectilePrefab;

    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    protected override void BeforeSkill()
    {
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
        _inputHandler.Public_StopMove();
    }
    protected override void TriggerSkill()
    {
        _projectilePrefab.LaunchProjectile(this.gameObject);
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}
