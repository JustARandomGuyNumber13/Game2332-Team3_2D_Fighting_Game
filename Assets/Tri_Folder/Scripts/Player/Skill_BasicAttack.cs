using UnityEngine;

public class Skill_BasicAttack : Skill
{
    [Header("Child class variable")]
    [SerializeField] private float attackRange;
    [SerializeField] private float attackOffset;
    
    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    protected override void BeforeSkill()
    { 
    
    }
    protected override void DuringSkill()
    {
        
    }
    protected override void AfterSkill() 
    {
    
    }
}
