using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    #region ~~ Variables ~~
    [SerializeField] protected SO_SkillStat _skillStat;
    protected bool _isCanUseSkill = true;
    #endregion

    #region ~~ Skill behavior handlers ~~
    public void ActivateSkill() 
    {
        if (!_skillStat.isPassiveSkill &&_isCanUseSkill)
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
    protected abstract void BeforeSkill();
    protected abstract void DuringSkill();
    protected abstract void AfterSkill();
    #endregion

    #region ~~ Coroutine handlers ~~
    protected IEnumerator SkillCoroutine()
    {
        // Delay before use skill
        float timer = 0;
        BeforeSkill();
        while (timer < _skillStat.skillDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // Using skill
        timer = 0;
        do
        {
            DuringSkill();
            timer += Time.deltaTime;
            yield return null;
        } while (timer < _skillStat.skillDuration);
        AfterSkill();

        // Cool down
        timer = 0;
        while (timer < _skillStat.skillCD)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _isCanUseSkill = true;
    }
    protected IEnumerator SkillCoolDownCoroutine()
    {
        float timer = 0;
        while (timer < _skillStat.skillCD)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _isCanUseSkill = true;
    }
    #endregion
}
