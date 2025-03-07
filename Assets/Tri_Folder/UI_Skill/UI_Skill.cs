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

    private void Start()
    {
        _countDownText.enabled = false;
        _skillLockImage.enabled = false;
    }

    public void Public_SetUp(Skill skill)
    {
        _cdDuration = skill.skillStat.skillCD;
        _skillImage.sprite = skill.skillStat.skillSprite;
        skill.OnTriggerSkillEvent.AddListener(UI_SkillCooldown);
    }

    private void UI_SkillCooldown()
    {
        _countDownText.enabled = true;
        _skillLockImage.enabled = true;
        StartCoroutine(UI_SkillCooldownCoroutine());
    }
    private IEnumerator UI_SkillCooldownCoroutine()
    {
        float timer = _cdDuration;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            _countDownText.text = timer + "";
        }

        _countDownText.enabled = false;
        _skillLockImage.enabled = false;
    }
}
