using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSpawn_Manager : MonoBehaviour
{
    [SerializeField] private SO_CharactersList _characterList;
    [SerializeField] private SO_PlayerSelection_Test _player1Selection, _player2Selection;
    //[SerializeField] private SO_PlayerSelection _player1Selection, _player2Selection;
    [SerializeField] private Transform _player1SpawnPosition, _player2SpawnPosition;
    [SerializeField] private Camera_Manager _camManager;

    private PlayerInputHandler  _player1, _player2;
    private Skill[] _player1SkillList, _player2SkillList;

    private void Start()
    {
        SpawnCharacters();
        ConnectComponents();
    }

    private void SpawnCharacters()
    {
        _player1 = Instantiate(_characterList.GetCharacterAt(_player1Selection.GetCharacterIndex()).characterPrefab, _player1SpawnPosition.position, _player1SpawnPosition.rotation).GetComponent<PlayerInputHandler>();
        _player2 = Instantiate(_characterList.GetCharacterAt(_player2Selection.GetCharacterIndex()).characterPrefab, _player2SpawnPosition.position, _player2SpawnPosition.rotation).GetComponent<PlayerInputHandler>();
    }

    private void ConnectComponents()
    { 
        // Set PlayerInputHandler.cs variable
        _player1.otherPlayer = _player2.transform;
        _player2.otherPlayer = _player1.transform;

        // Set Camera_Manger.cs variable
        _camManager.Public_AssignPlayer(_player1.transform, _player2.transform);

        // Set corresponding action map (Action map, not Action Input Asset) assume it will always be Fighting Action Input Asset as set in prefab
        _player1.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player1");
        _player2.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player2");

        // Get skill list (A list of Skill.cs components that attach to the prefab)
        _player1SkillList = _player1.GetComponents<Skill>();
        _player2SkillList = _player2.GetComponents<Skill>();

        // Assign player's selected skill to either UnityEvent (from PlayerInputHandler.cs) or toggle active (from Skill.cs) if skill type is Passive
        AssignSkills(_player1SkillList, _player1, _player1Selection);
        AssignSkills(_player2SkillList, _player2, _player2Selection);
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
    private void Helper_PassiveSkillCheck(Skill skill, UnityEvent skillEvent)   // Toggle passive skill's active to true, or assign to event if it's not a passive skill
    {
        if (skill.skillStat.isPassiveSkill)
            skill.isPassiveSkillActive = true;
        else
            skillEvent.AddListener(skill.ActivateSkill);
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
}