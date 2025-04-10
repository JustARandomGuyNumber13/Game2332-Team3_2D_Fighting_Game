using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSpawnHandler : MonoBehaviour
{
    [Header("Basic requirements")]
    [SerializeField] private SO_CharactersList characterList;
    [SerializeField] private SO_PlayerSelection p1Selection, p2Selection;
    [SerializeField] private Transform p1SpawnPos, p2SpawnPos;
    [SerializeField] private float spawnPosOffsetY;
    [SerializeField] private UI_Skill[] p1SkillBoxes, p2SkillBoxes;

    [SerializeField] private UnityEvent<GameObject, GameObject> OnSetUpEvent;
    private GameObject _player1, _player2;

    private void Start()
    {
        SpawnCharacters();
        SetUpCharacters();
    }

    private void SpawnCharacters()
    {
        _player1 = Instantiate(characterList.GetCharacterAt(p1Selection.CharacterIndex).characterPrefab, p1SpawnPos.position + Vector3.up * spawnPosOffsetY, p1SpawnPos.rotation);
        p1SpawnPos.transform.SetParent(_player1.transform);

        _player2 = Instantiate(characterList.GetCharacterAt(p2Selection.CharacterIndex).characterPrefab, p2SpawnPos.position + Vector3.up * spawnPosOffsetY, p2SpawnPos.rotation);
        p2SpawnPos.transform.SetParent(_player2.transform);

        AudioPlayer._instance.RegisteredPlayers(_player1, _player2); //Registering players to use skill audio

        //Register plays to use hurt audio
        var p1Healthhandler = _player1.GetComponent<PlayerHealthHandler>();
        var p2Healthhandler = _player2.GetComponent<PlayerHealthHandler>();
        AudioPlayer._instance.RegisteredHealthEvents(p1Healthhandler, p2Healthhandler);
    }
    private void SetUpCharacters()
    {
        /* Set up player's Action Maps */
        _player1.GetComponent<PlayerInput>().SwitchCurrentActionMap(Global.playerOneInputMap);
        _player2.GetComponent<PlayerInput>().SwitchCurrentActionMap(Global.playerTwoInputMap);

        /* Set up players' input handlers */
        PlayerInputHandler p1Input = _player1.GetComponent<PlayerInputHandler>();
        PlayerInputHandler p2Input = _player2.GetComponent<PlayerInputHandler>();
        p1Input.otherPlayer = p2Input.transform;
        p2Input.otherPlayer = p1Input.transform;

        /* Assign players' selected skills */
        Skill[] p1SkillList = _player1.GetComponents<Skill>();
        Skill[] p2SkillList = _player2.GetComponents<Skill>();
        AssignSkills(p1SkillList, p1SkillBoxes, p1Input, p1Selection);
        AssignSkills(p2SkillList, p2SkillBoxes, p2Input, p2Selection);

        OnSetUpEvent?.Invoke(_player1, _player2);
    }

    private void AssignSkills(Skill[] skillList, UI_Skill[] skillBoxList, PlayerInputHandler inputHandler, SO_PlayerSelection playerSelection)
    {
        if (skillBoxList.Length == 0) return;

        Skill skillOne = Helper_GetSkillFromPrefab(skillList, playerSelection.SkillOneIndex, playerSelection);
        Skill skillTwo = Helper_GetSkillFromPrefab(skillList, playerSelection.SkillTwoIndex, playerSelection);
        Skill skillThree = Helper_GetSkillFromPrefab(skillList, playerSelection.SkillThreeIndex, playerSelection);

        skillBoxList[0].Public_SetUp(skillOne);
        skillBoxList[1].Public_SetUp(skillTwo);
        skillBoxList[2].Public_SetUp(skillThree);

        Helper_PassiveSkillCheck(skillOne, inputHandler.OnSkillOneEvent);
        Helper_PassiveSkillCheck(skillTwo, inputHandler.OnSkillTwoEvent);
        Helper_PassiveSkillCheck(skillThree, inputHandler.OnSkillThreeEvent);
    }
    private Skill Helper_GetSkillFromPrefab(Skill[] skillList, int selectionSkillIndex, SO_PlayerSelection playerSelection) // Loop through every component Skill.cs in the prefab, then return correct skill that match the selected skill index by comparing SO_SkillStat
    {
        for (int i = 0; i < skillList.Length; i++)
        {
            SO_SkillStat prefabSkillStat = skillList[i].skillStat;
            SO_SkillStat characterSkillStat = characterList.GetCharacterAt(playerSelection.CharacterIndex).skills[selectionSkillIndex];

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