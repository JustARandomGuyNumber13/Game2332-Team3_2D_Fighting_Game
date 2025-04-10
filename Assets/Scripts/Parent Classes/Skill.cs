using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    #region ~~ Variables ~~
    [Header("Require Components")]
    public SO_SkillStat skillStat;

    [Header("Unity Events")]
    public UnityEvent OnBeforeSkillEvent;
    public UnityEvent OnTriggerSkillEvent;
    public UnityEvent OnAfterSkillEvent;

    protected bool _isCanUseSkill = true;
    protected bool _isPassiveSkillActive;

    public bool isPassiveSkillActive {set { _isPassiveSkillActive = value; } }
    #endregion

    #region ~~ Skill behavior handlers ~~
    public void ActivateSkill() 
    {
        if ((!skillStat.isPassiveSkill && _isCanUseSkill) || (skillStat.isPassiveSkill && _isPassiveSkillActive))
        {
            _isCanUseSkill = false;
            //Debug.Log(GetType().Name, gameObject);
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
        /* Delay before use skill */
        BeforeSkill();
        OnBeforeSkillEvent?.Invoke();
        if(skillStat.skillDelay != 0)
            yield return new WaitForSeconds(skillStat.skillDelay);

        /* Using skill */
        TriggerSkill();
        OnTriggerSkillEvent?.Invoke();
        float timer = 0;
        while (timer < skillStat.skillDuration)
        {
            yield return null;
            DuringSkill(timer);
            timer += Time.deltaTime;
        }
        AfterSkill();
        OnAfterSkillEvent?.Invoke();

        /* Cool down */
        StartCoroutine (SkillCoolDownCoroutine());
    }
    protected IEnumerator SkillCoolDownCoroutine()
    {
        if(skillStat.skillCD != 0)
            yield return new WaitForSeconds(skillStat.skillCD);
        _isCanUseSkill = true;
    }
    #endregion
}
