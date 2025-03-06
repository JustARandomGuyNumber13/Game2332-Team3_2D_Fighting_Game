using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSpawn_Manager : MonoBehaviour
{
    [Header("Basic requirements")]
    [SerializeField] private SO_CharactersList _characterList;
    [SerializeField] private SO_PlayerSelection_Test _player1Selection, _player2Selection;
    //[SerializeField] private SO_PlayerSelection _player1Selection, _player2Selection;
    [SerializeField] private Transform _player1SpawnPosition, _player2SpawnPosition;

    [SerializeField] private UnityEvent<GameObject, GameObject> OnSetUpEvent;
    private GameObject  _player1, _player2;

    private void Start()
    {
        SpawnCharacters();
        SetUpCharacters();
    }

    private void SpawnCharacters()
    {
        _player1 = Instantiate(_characterList.GetCharacterAt(_player1Selection.GetCharacterIndex()).characterPrefab, _player1SpawnPosition.position, _player1SpawnPosition.rotation);
        _player2 = Instantiate(_characterList.GetCharacterAt(_player2Selection.GetCharacterIndex()).characterPrefab, _player2SpawnPosition.position, _player2SpawnPosition.rotation);
    }
    private void SetUpCharacters()
    {
        /* Set up player's Action Maps */
        _player1.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player1");
        _player2.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player2");

        /* Set up players' input handlers */
        PlayerInputHandler p1Input = _player1.GetComponent<PlayerInputHandler>();
        PlayerInputHandler p2Input = _player2.GetComponent<PlayerInputHandler>();
        p1Input.otherPlayer = p2Input.transform;
        p2Input.otherPlayer = p1Input.transform;

        /* Assign players' selected skills */
        Skill[] p1SkillList = _player1.GetComponents<Skill>();
        Skill[] p2SkillList = _player2.GetComponents<Skill>();
        AssignSkills(p1SkillList, p1Input, _player1Selection);
        AssignSkills(p2SkillList, p2Input, _player2Selection);

        OnSetUpEvent?.Invoke(_player1, _player2);
    }

    private void AssignSkills(Skill[] skillList, PlayerInputHandler inputHandler, SO_PlayerSelection_Test playerSelection)
    //private void AssignSkills(Skill[] skillList, PlayerInputHandler inputHandler, SO_PlayerSelection playerSelection)
    {
        Skill skillOne = Helper_GetSkillFromPrefab(skillList, playerSelection._skillOneIndex, playerSelection);
        Skill skillTwo = Helper_GetSkillFromPrefab(skillList, playerSelection._skillTwoIndex, playerSelection);
        Skill skillThree = Helper_GetSkillFromPrefab(skillList, playerSelection._skillThreeIndex, playerSelection);

        //Skill skillOne = Helper_GetSkillIndex(skillList, playerSelection.GetSkillOneIndex(), playerSelection);
        //Skill skillTwo = Helper_GetSkillIndex(skillList, playerSelection.GetSkillTwoIndex(), playerSelection);
        //Skill skillThree = Helper_GetSkillIndex(skillList, playerSelection.GetSkillThreeIndex(), playerSelection);

        Helper_PassiveSkillCheck(skillOne, inputHandler.OnSkillOneEvent);
        Helper_PassiveSkillCheck(skillTwo, inputHandler.OnSkillTwoEvent);
        Helper_PassiveSkillCheck(skillThree, inputHandler.OnSkillThreeEvent);
    }
    private Skill Helper_GetSkillFromPrefab(Skill[] skillList, int selectionSkillIndex, SO_PlayerSelection_Test playerSelection)
    //private Skill Helper_GetSkillIndex(Skill[] skillList, int selectionSkillIndex, SO_PlayerSelection playerSelection) // Loop through every component Skill.cs in the prefab, then return correct skill that match the selected skill index by comparing SO_SkillStat
    {
        for (int i = 0; i < skillList.Length; i++)
        {
            SO_SkillStat prefabSkillStat = skillList[i].skillStat;
            SO_SkillStat characterSkillStat = _characterList.GetCharacterAt(playerSelection._characterIndex).skills[selectionSkillIndex];
            //SO_SkillStat characterSkillStat = _characterList.GetCharacterAt(playerSelection.GetCharacterIndex()).skills[selectionSkillIndex];

            if (prefabSkillStat.Equals(characterSkillStat))
                return skillList[i];
        }
        return null;
    }
    private void Helper_PassiveSkillCheck(Skill skill, UnityEvent skillEvent)   // Toggle passive skill's active to true, or assign to event if it's not a passive skill
    {
        if (skill.skillStat.isPassiveSkill)
            skill.isPassiveSkillActive = true;
        else
            skillEvent.AddListener(skill.ActivateSkill);
    }
}