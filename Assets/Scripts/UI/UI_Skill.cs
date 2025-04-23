using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour
{
    private float _cdDuration;
    [SerializeField] private TMP_Text _countDownText;
    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _skillLockImage;
    private float timer;
    private bool isCD;

    private void Start()
    {
        _countDownText.enabled = false;
        _skillLockImage.enabled = false;
    }
    private void Update()
    {
        if (isCD)
        {
            timer -= Time.deltaTime;
            _countDownText.text = Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                isCD = false;
                _countDownText.enabled = false;
                _skillLockImage.enabled = false;
            }
        }
    }
    public void Public_SetUp(Skill skill)
    {
        _cdDuration = skill.skillStat.skillCD + skill.skillStat.skillDuration;
        _skillImage.sprite = skill.skillStat.skillSprite;
        if(!skill.skillStat.isPassiveSkill)
            skill.OnTriggerSkillEvent.AddListener(UI_SkillCooldown);
    }

    private void UI_SkillCooldown()
    {
        timer = _cdDuration;
        _countDownText.enabled = true;
        _skillLockImage.enabled = true;
        isCD = true;
    }
}
