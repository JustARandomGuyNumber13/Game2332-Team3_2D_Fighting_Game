using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Security.Cryptography;
using UnityEditor.Playables;
using System;
using UnityEngine.Events;

public class T_SM_UI_PlayerSelection : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private T_SO_PlayerSelection _playerData;  // Adjust skill selection amount in: T_SO_PlayerSelection (instance), 
    [SerializeField] private T_SM_UI_Skill_List _skillList;
    [SerializeField] private T_SM_UI_Skill_Slot[] _skillSlotList;

    [Header("UI Components")]
    [SerializeField] private Image _skillImage;
    [SerializeField] private TMP_Text _skillType;
    [SerializeField] private TMP_Text _skillDescription;

    [Header("Other")]
    [SerializeField] private GameObject _readyObject;
    [SerializeField] private UnityEvent<bool> OnReadyCheck;

    private int[] _skillIndexList;
    private T_SM_UI_Skill_Content _curSkill;
    private int _curSkillIndex;
    private int _curSkillSlotIndex;

    private enum SelectionState
    { 
        ViewSkill,
        AssignSkill,
        ReadyCheck
        //,ChooseCharacterColor
    }
    private SelectionState _selectionState;

    private void Start()
    {
        GetSkill(0);
        _selectionState = SelectionState.ViewSkill;
        _skillIndexList = new int[_skillSlotList.Length];

        for (int i = 0; i < _skillIndexList.Length; i++)
            _skillIndexList[i] = -1;
    }


    /* Ready check handlers */
    private void ReadyCheck()
    {
        if (_selectionState != SelectionState.ReadyCheck)
        {
            _selectionState = SelectionState.ReadyCheck;
            for (int i = 0; i < _skillIndexList.Length; i++)
            {
                if (_skillIndexList[i] == -1)
                {
                    Debug.LogError("Not all skills are selected yet!");
                    _skillSlotList[_curSkillSlotIndex].SetColor(false);
                    _selectionState = SelectionState.ViewSkill;
                    return;
                }
                _playerData.selectedSkillList[i] = _skillIndexList[i];
            }
            OnReadyCheck?.Invoke(_selectionState == SelectionState.ReadyCheck);
            _readyObject.SetActive(true);
            Debug.Log("Save data successfully");
        }
        else
        {
            _readyObject.SetActive(false);
            _selectionState = SelectionState.ViewSkill;
        }
    }
    public void Public_ReadyCheck(bool isOtherPlayerReady)
    {
        if (_selectionState == SelectionState.ReadyCheck && isOtherPlayerReady)
            SceneManager.LoadScene(Global.gamePlayScene);
    }


    /* Assign skill handlers */
    private void ToggleAssignSkill()
    {
        if (_selectionState == SelectionState.ReadyCheck)
        {
            return;
        }
        else if (_selectionState == SelectionState.ViewSkill)
        {
            _selectionState = SelectionState.AssignSkill;
            _curSkillSlotIndex = 0;
            GetSkillSlot(0);
        }
        else if (_selectionState == SelectionState.AssignSkill)
        {
            Helper_AssignSkill();
            _selectionState = SelectionState.ViewSkill;
        }
    }
    private void GetSkillSlot(int direction)
    {
        if (_selectionState != SelectionState.AssignSkill) return;

        if (direction != 0)
            _skillSlotList[_curSkillSlotIndex].SetColor(false);

        _curSkillSlotIndex += direction;

        if (_curSkillSlotIndex >= _skillList.listLength)
            _curSkillSlotIndex = 0;
        else if (_curSkillSlotIndex < 0)
            _curSkillSlotIndex = _skillList.listLength - 1;

        Helper_UpdateSkillSlotUI();
    }
    private void Helper_AssignSkill()
    {
        for (int i = 0; i < _skillSlotList.Length; i++)
            if (_skillSlotList[i].skillImage.sprite == _curSkill.skill.skillSprite)
            {
                Helper_ClearSkillSlot(i);
                break;
            }

        _skillSlotList[_curSkillSlotIndex].skillImage.sprite = _curSkill.skill.skillSprite;
        _skillSlotList[_curSkillSlotIndex].text.text = (_curSkillIndex + 1) + "";
        _skillSlotList[_curSkillSlotIndex].text.enabled = true;
        _skillSlotList[_curSkillSlotIndex].SetColor(false);
        _skillIndexList[_curSkillSlotIndex] = _curSkillIndex;
    }
    private void Helper_UpdateSkillSlotUI()
    {
        _skillSlotList[_curSkillSlotIndex].SetColor(true);
    }
    private void Helper_ClearSkillSlot(int index)
    {
        _skillSlotList[index].text.enabled = false;
        _skillSlotList[index].skillImage.sprite = null;
        _skillIndexList[index] = -1;
    }


    /* View skill handlers */
    private void GetSkill(int direction)
    {
        if (_selectionState != SelectionState.ViewSkill) return;

        _skillList.skillListUI[_curSkillIndex].image.enabled = false;
        _curSkillIndex += direction;

        if (_curSkillIndex >= _skillList.listLength)
            _curSkillIndex = 0;
        else if(_curSkillIndex < 0)
            _curSkillIndex = _skillList.listLength - 1;

        _curSkill = _skillList.skillListUI[_curSkillIndex];
        Helper_UpdateSkillViewUI();
    }

    private void Helper_UpdateSkillViewUI()
    { 
        _curSkill.image.enabled = true;
        _skillDescription.text = _curSkill.skill.skillDescription;
        _skillImage.sprite = _curSkill.skill.skillSprite;

        if (_curSkill.skill.isPassiveSkill)
            _skillType.text = "Ability type: Passive";
        else
            _skillType.text = "Ability type: Active" + "\n" + "Cooldown duration: " + _curSkill.skill.skillCD + "s";
    }


    /* Input handlers */
    private void OnJump(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) != 0) GetSkill(-1);
    }
    private void OnCrouch(InputValue value) 
    {
        if(Mathf.Ceil(value.Get<float>()) != 0) GetSkill(1);
    }
    private void OnAttack(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) != 0) ToggleAssignSkill();
    }
    private void OnMove(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) != 0) GetSkillSlot((int)Mathf.Ceil(value.Get<float>()));
    }
    private void OnSkillOne(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) != 0) ReadyCheck();
    }


    private void InspectorCheck()
    {
        if (_playerData.selectedSkillList.Length != _skillSlotList.Length)
            Debug.LogError("Skill selection (T_SO_PlayerSelection) amount doesn't match skill slot amount (T_SM_UI_Skill_Slot)");
    }
}