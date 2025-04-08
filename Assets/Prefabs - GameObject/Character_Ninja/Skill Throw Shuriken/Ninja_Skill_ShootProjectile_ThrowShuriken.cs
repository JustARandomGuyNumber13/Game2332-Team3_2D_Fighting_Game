using UnityEngine;

public class Ninja_Skill_ShootProjectile_ThrowShuriken : Skill  // Skill_ShootProjectile_Template.cs
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
<<<<<<< HEAD
        _inputHandler.Public_StopMove();
    }
    protected override void TriggerSkill()
    {
=======
    }
    protected override void TriggerSkill()
    {
        Debug.Log("Ninja Throw Shuriken", gameObject);
>>>>>>> Aaron-Branch
        _projectilePrefab.LaunchProjectile(this.gameObject);
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}
