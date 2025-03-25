using System.Collections.Generic;
using UnityEngine;

public class T_SM_UI_Skill_List : MonoBehaviour
{
    [SerializeField] private T_SO_SkillList _skillList;
    [SerializeField] private T_SM_UI_Skill_Content _skillPrefabUI;
    public List<T_SM_UI_Skill_Content> skillListUI { get; set; }
    public int listLength { get; private set; }

    private void Awake()
    {
        skillListUI = new List<T_SM_UI_Skill_Content> ();
        _skillPrefabUI.gameObject.SetActive (false);
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < _skillList.skillList.Length; i++)
        {
            listLength++;
            T_SM_UI_Skill_Content curSkillContent = Instantiate(_skillPrefabUI);
            curSkillContent.skill = _skillList.skillList[i];
            curSkillContent.text.text = (i + 1) + ". " + curSkillContent.skill.skillName;
            curSkillContent.transform.SetParent(transform);
            curSkillContent.gameObject.SetActive(true);
            skillListUI.Add(curSkillContent);
        }
    }
}
