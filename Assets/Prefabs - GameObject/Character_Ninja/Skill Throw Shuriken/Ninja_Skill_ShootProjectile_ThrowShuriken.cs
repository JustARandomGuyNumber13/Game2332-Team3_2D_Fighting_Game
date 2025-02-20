using UnityEngine;

public class Ninja_Skill_ShootProjectile_ThrowShuriken : Skill
{
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
    }
    protected override void TriggerSkill()
    {
        Debug.Log("Ninja Throw Shuriken", gameObject);
        _projectilePrefab.LaunchProjectile(this.gameObject);
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}
