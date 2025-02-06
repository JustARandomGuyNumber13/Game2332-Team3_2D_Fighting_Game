using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public Text nameText;
    public SpriteRenderer artworkSprite;
    public GameObject[] skillGameObject;

    public Image[] activeSkillSlots;
    public Image[] selectableSkillsSlots;
    public Image[] skillSprites;
    private Image[] navigatableArray;

    private int navigationIndex = 0;
    private int selectedOption = 0;
    private int selectedActiveIndex;
    private int selectedSkillIndex;

    public enum selectionMode { characterSelection, activeSlot, selectableSlot }

    public selectionMode currentSelectionMode = selectionMode.characterSelection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }

        else
        {
            Load();
        }

        UpdateCharacter(selectedOption);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    BackOption();
                    break;
                case selectionMode.activeSlot: //update the index of the selectedActiveIndex
                    break;
                case selectionMode.selectableSlot: //update the index of the selectedSkillIndex
                    break; 
            }

               
            //BackOption();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //NextOption();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    currentSelectionMode = selectionMode.activeSlot;
                    break;
                case selectionMode.activeSlot:
                    //get the index
                    currentSelectionMode = selectionMode.selectableSlot;
                    break;
                case selectionMode.selectableSlot:
                    activeSkillSlots[selectedActiveIndex].sprite = skillSprites[selectedSkillIndex].sprite;
                    selectableSkillsSlots[selectedSkillIndex].sprite = null;
                    break;

            }
        }


    }

    public void NextOption()
    {
        selectedOption++;

        if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
