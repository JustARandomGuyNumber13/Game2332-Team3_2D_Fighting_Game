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
    private GameObject p1, p2;
    private PlayerInput p1InputMap, p2InputMap;

    public void Public_SetUp()
    {
        SpawnCharacters();
        SetUpCharacters();
    }
    public void Public_StartGame()
    {
        p1InputMap.enabled = true;
        p2InputMap.enabled = true;
    }


    private void SpawnCharacters()
    {
        p1 = Instantiate(characterList.GetCharacterAt(p1Selection.CharacterIndex).characterPrefab, p1SpawnPos.position + Vector3.up * spawnPosOffsetY, p1SpawnPos.rotation);
        p1SpawnPos.transform.SetParent(p1.transform);

        p2 = Instantiate(characterList.GetCharacterAt(p2Selection.CharacterIndex).characterPrefab, p2SpawnPos.position + Vector3.up * spawnPosOffsetY, p2SpawnPos.rotation);
        p2SpawnPos.transform.SetParent(p2.transform);
    }
    private void SetUpCharacters()
    {
        /* Set up player's Action Maps */
        p1InputMap = p1.GetComponent<PlayerInput>();
        p1InputMap.defaultActionMap = Global.playerOneInputMap;
        p1InputMap.enabled = false;

        p2InputMap = p2.GetComponent<PlayerInput>();
        p2InputMap.defaultActionMap = Global.playerTwoInputMap;
        p2InputMap.enabled = false;

        /* Assign player tag */
        p1.tag = Global.playerOneTag;
        p2.tag = Global.playerTwoTag;

        /* Set up players' input handlers */
        PlayerInputHandler p1Input = p1.GetComponent<PlayerInputHandler>();
        PlayerInputHandler p2Input = p2.GetComponent<PlayerInputHandler>();
        p1Input.otherPlayer = p2Input.transform;
        p2Input.otherPlayer = p1Input.transform;

        /* Assign players' selected skills */
        Skill[] p1SkillList = p1.GetComponents<Skill>();
        Skill[] p2SkillList = p2.GetComponents<Skill>();
        AssignSkills(p1SkillList, p1SkillBoxes, p1Input, p1Selection);
        AssignSkills(p2SkillList, p2SkillBoxes, p2Input, p2Selection);

        OnSetUpEvent?.Invoke(p1, p2);
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

        Helper_EnableSkill(skillOne, inputHandler.OnSkillOneEvent);
        Helper_EnableSkill(skillTwo, inputHandler.OnSkillTwoEvent);
        Helper_EnableSkill(skillThree, inputHandler.OnSkillThreeEvent);
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
    private void Helper_EnableSkill(Skill skill, UnityEvent skillEvent)   // Toggle passive skill's active to true, or assign to event if it's not a passive skill
    {
        skill.enabled = true;
        if (skill.skillStat.isPassiveSkill)
            skill.isPassiveSkillActive = true;
        else
            skillEvent.AddListener(skill.ActivateSkill);
    }
}