using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class T_GP_PlayerSpawn_Manager : MonoBehaviour
{
    [Header("Basic requirements")]
    [SerializeField] private T_SO_SkillList _skillList;
    [SerializeField] private T_SO_PlayerSelection _p1SkillSelect, _p2SkillSelect;
    [SerializeField] private PlayerInputHandler _p1InputHandler, _p2InputHandler;
    [SerializeField] private T_GP_UI_Skill[] _p1SkillBoxes, _p2SkillBoxes;

    [SerializeField] private UnityEvent<GameObject, GameObject> OnSetUpEvent;

    private void Start()
    {
        InspectorCheck();
        AssignSkills(_p1InputHandler, _p1SkillSelect, _p1SkillBoxes);
        AssignSkills(_p2InputHandler, _p2SkillSelect, _p2SkillBoxes);
        OnSetUpEvent?.Invoke(_p1InputHandler.gameObject, _p2InputHandler.gameObject);
    }
    private void AssignSkills(PlayerInputHandler inputHandler, T_SO_PlayerSelection skillSelect, T_GP_UI_Skill[] skillBoxes)
    {
        int[] skillListIndex = skillSelect.selectedSkillList;
        for (int i = 0; i < skillBoxes.Length; i++)
        {
            T_SO_SkillStat curSkillStat = _skillList.skillList[skillListIndex[i]];   // Get skill from skill list
            T_GP_Skill curSkill = Instantiate(curSkillStat.skillPrefab, inputHandler.gameObject.transform).GetComponent<T_GP_Skill>();
            curSkill.gameObject.transform.SetParent(inputHandler.gameObject.transform);
            
            if (curSkillStat.isPassiveSkill)
            {
                curSkill.isPassiveSkillActive = true;
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
                    case 3:
                        inputHandler.OnSkillFourEvent.AddListener(curSkill.ActivateSkill);
                        break;
                    case 4:
                        inputHandler.OnSkillFiveEvent.AddListener(curSkill.ActivateSkill); 
                        break;
                }
            }
            skillBoxes[i].Public_SetUp(curSkill);
        }
    }
    private void InspectorCheck()
    {
        if (_p1SkillSelect.selectedSkillList.Length !=  _p1SkillBoxes.Length)
        {
            Debug.LogError(GetType().Name + ".cs inspector check failed, player one skills amount don't match skill slots amount \n" +
                "Skill amount: " + _p1SkillSelect.selectedSkillList.Length +
                ", skill slot amount: " + _p1SkillBoxes.Length, gameObject);
        }
        if (_p2SkillSelect.selectedSkillList.Length != _p2SkillBoxes.Length)
        {
            Debug.LogError(GetType().Name + ".cs inspector check failed, player two skills amount don't match skill slots amount \n" +
                "Skill amount: " + _p2SkillSelect.selectedSkillList.Length +
                ", skill slot amount: " + _p2SkillBoxes.Length, gameObject);
        }
    }
}