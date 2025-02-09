using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private SO_PlayerSelection _playerSelection;
    [SerializeField] private SO_CharactersList _charactersList;

    [Header("UI components")]
    [SerializeField] private Image _characterImage; // Image that  represent the character
    [SerializeField] private Image[] _skillImages;  // Images that represent 5 skills of each character
    [SerializeField] private Image[] _skillSlotImages;  // Images that represent 3 select skills

    private int characterIndex;
    private int skillOne, skillTwo, skillThree;
    private SO_CharacterStat _curCharacter;

    private void Start()
    {
        _curCharacter = _charactersList.GetCharacterAt(characterIndex);
        Helper_ResetSkillSlots();
        Helper_ChangeCharacter_UpdateUI();
    }


    #region ~~ Functions for Input control ~~
    public void ChangeCharacter(float direction)
    {
        if (direction > 0)
        {
            characterIndex++;
            if (characterIndex >= _charactersList.size)
                characterIndex = 0;
        }
        else if (direction < 0)
        {
            characterIndex--;
            if (characterIndex < 0)
                characterIndex = _charactersList.size - 1;
        }

        if (direction != 0)
        {
            _curCharacter = _charactersList.GetCharacterAt(characterIndex);
            Helper_ResetSkillSlots();
            Helper_ChangeCharacter_UpdateUI();
        }
    }
    private void AssignSkillSlot(int skillSlot, int skillIndex) // skillSlot 1-3, skillIndex 1-5
    {
        switch (skillSlot)
        {
            case 1: skillOne = skillIndex;  break;
            case 2: skillTwo = skillIndex; break;
            case 3: skillThree = skillIndex; break;
        }
        _skillSlotImages[skillSlot - 1].sprite = _curCharacter.skills[skillIndex].skillSprite; // Update skill slot's UI
    }
    #endregion


    #region ~~ Public functions for UI buttons/handlers ~~
    public void UI_SaveData()
    {
        _playerSelection.SaveData(characterIndex, skillOne, skillTwo, skillThree);
    }

    /*
     * Temporary section for scene template//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     */
    private bool isAssigningSkill;
    private int assigningSkillSlotIndex;

    public void UI_ToggleAssigningSkilll(int skillSlotIndex)
    {
        if (!isAssigningSkill)
        {
            isAssigningSkill = true;
            assigningSkillSlotIndex = skillSlotIndex;
            print("Assigning skill for skill slot " + skillSlotIndex);
        }
        else
        {
            if (assigningSkillSlotIndex == skillSlotIndex)
            {
                isAssigningSkill = false;
                print("Toggle off assigning skill");
            }
        }
    }
    public void UI_AssignSkill(int skillIndex)
    {
        if (isAssigningSkill)
        {
            AssignSkillSlot(assigningSkillSlotIndex, skillIndex);
            isAssigningSkill = false;
            print("Skill slot " + assigningSkillSlotIndex + " is assigned to skill " + skillIndex);
        }
    }
    /*
     * End temporary section////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     */

    #endregion


    #region ~~ Helper handlers ~~
    private void Helper_ChangeCharacter_UpdateUI()  // Change all sprites, skill slots swap back to 3 default skills
    {
        _characterImage.sprite = _curCharacter.characterSprite;
        for (int i = 0; i < 5; i++)
        {
            _skillImages[i].sprite = _curCharacter.skills[i + 1].skillSprite;
            if (i < 3)
                _skillSlotImages[i].sprite = _curCharacter.skills[i + 1].skillSprite;
        }
    }
    private void Helper_ResetSkillSlots()  
    {
        skillOne = 1;
        skillTwo = 2;
        skillThree = 3;
    }
    #endregion


    #region ~~ Input handlers ~~

    #endregion
}
