using UnityEngine;

public class Skill_Passive_Template : Skill
{
    // *Note that SO_SkillStat's variable "isPassiveSkill" must be checked (true)

    // Exclusive variables for this skill if have
    [Header("Skill exclusive variables")]
    [SerializeField] private bool dontAskMe;

    // Private variables to get either PlayerInputHandler or PlayerHealthHandler

    private void Awake()
    {
        // Get private components if have
    }

    // Either use TriggerSkill() or DuringSkill() depend on the skill behavior
    protected override void TriggerSkill()
    {
        
    }
    protected override void DuringSkill(float timer)    // timer pass from the SkillCoroutine (Skill.cs)
    {
        
    }

    // Last step, assign skill in the corresponding event in Character's prefab -> Inspector -> PlayerInputHandler.cs

    /* How will the passive skill get call? 
     * When prefab is SPAWN with this PASSIVE skill SELECTED
     * Skill.cs will check "isPassiveSkillActive" as true
     * Then whichever function it connected to will be able to call this action
     */
}
