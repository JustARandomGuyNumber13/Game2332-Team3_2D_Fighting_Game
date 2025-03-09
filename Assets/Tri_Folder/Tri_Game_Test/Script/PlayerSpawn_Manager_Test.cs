using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSpawn_Manager_Test : MonoBehaviour
{
    [Header("Basic requirements")]
    [SerializeField] private SO_Skill_List_Test _skillList;
    [SerializeField] private SO_PlayerSelection_Test_2 _p1SkillSelect, _p2SkillSelect;
    [SerializeField] private PlayerInputHandler _p1InputHandler, _p2InputHandler;
    [SerializeField] private UI_Skill_Test[] _p1SkillBoxes, _p2SkillBoxes;

    [SerializeField] private UnityEvent<GameObject, GameObject> OnSetUpEvent;
    private GameObject  _player1, _player2;

    private void Start()
    {
        InspectorCheck();

        AssignSkills(_p1InputHandler, _p1SkillSelect, _p1SkillBoxes);
        AssignSkills(_p2InputHandler, _p2SkillSelect, _p2SkillBoxes);
        OnSetUpEvent?.Invoke(_p1InputHandler.gameObject, _p2InputHandler.gameObject);
    }
    private void AssignSkills(PlayerInputHandler inputHandler, SO_PlayerSelection_Test_2 skillSelect, UI_Skill_Test[] skillBoxes)
    {
        int[] skillListIndex = skillSelect.selectedSkillList;
        for (int i = 0; i < skillListIndex.Length; i++)
        {
            SO_SkillStat_Test curSkillStat = _skillList.skillList[skillListIndex[i]];   // Get skill from skill list
            Skill_Test curSkill = Instantiate(curSkillStat.skillPrefab, inputHandler.gameObject.transform).GetComponent<Skill_Test>();
            curSkill.gameObject.transform.SetParent(inputHandler.gameObject.transform);
            
            if (curSkillStat.isPassiveSkill)
            {
                curSkill.isPassiveSkillActive = true;
                skillBoxes[i].Public_SetUp(curSkill);
            }
            else
            {
                switch (i)
                {
                    case 0:
                        inputHandler.OnSkillOneEvent.AddListener(curSkill.ActivateSkill);
                        break;
                    case 1:
                        inputHandler.OnSkillTwoEvent.AddListener(curSkill.ActivateSkill);
                        break;
                    case 2:
                        inputHandler.OnSkillThreeEvent.AddListener(curSkill.ActivateSkill);
                        break;
                }
                
                skillBoxes[i].Public_SetUp(curSkill);
            }
        }
    }
    private void InspectorCheck()
    {
        if (_p1SkillSelect.selectedSkillList.Length != 3 ||  _p1SkillBoxes.Length != 3)
        {
            Debug.Log(_p1SkillSelect.selectedSkillList.Length + " : " + _p1SkillBoxes.Length);
            Debug.LogError("Only 3 skills are available currently");
            return;
        }
        if (_p2SkillSelect.selectedSkillList.Length != 3 || _p1SkillBoxes.Length != 3)
        {
            Debug.LogError("Only 3 skills are available currently");
            return;
        }
    }
}