using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    #region ~~ Variables ~~
    public SO_SkillStat skillStat;
    protected bool _isCanUseSkill = true;

    [Tooltip("Only for passive skill")]
    public bool isPassiveSkillActive;
    #endregion

    #region ~~ Skill behavior handlers ~~
    public void ActivateSkill() 
    {
        if ((!skillStat.isPassiveSkill && _isCanUseSkill) || (skillStat.isPassiveSkill && isPassiveSkillActive))
        {
            _isCanUseSkill = false;
            StartCoroutine(SkillCoroutine());
        }
    }
    public void DeactivateSkill() 
    {
        StopAllCoroutines();
        StartCoroutine(SkillCoolDownCoroutine());
    }
    protected virtual void BeforeSkill() { }
    protected virtual void DuringSkill(float timer) { }
    protected virtual void TriggerSkill() { }
    protected virtual void AfterSkill() { }
    #endregion

    #region ~~ Coroutine handlers ~~
    protected IEnumerator SkillCoroutine()
    {
        // Delay before use skill
        float timer = 0;
        BeforeSkill();
        while (timer < skillStat.skillDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Using skill
        TriggerSkill();
        timer = 0;
        while (timer < skillStat.skillDuration)
        {
            DuringSkill(timer);
            timer += Time.deltaTime;
            yield return null;
        }
        AfterSkill();

        // Cool down
        timer = 0;
        while (timer < skillStat.skillCD)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _isCanUseSkill = true;
    }
    protected IEnumerator SkillCoolDownCoroutine()
    {
        float timer = 0;
        while (timer < skillStat.skillCD)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _isCanUseSkill = true;
    }
    #endregion
}
