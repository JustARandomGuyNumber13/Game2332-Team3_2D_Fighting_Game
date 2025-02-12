using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.InputSystem.Interactions;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;

public class SelectionUI : MonoBehaviour
{
    //public CharacterDatabase characterDB;
    public SO_CharactersList characterList;

    public Text nameText;
    public SpriteRenderer artworkSprite;
   
    public Image[] activeSkillSlot;
    public Image[] selectableSkillSlot;

    [SerializeField]
    private Image activeSlotHighlight;
    [SerializeField]
    private Image selectedSkillHighlight;

    private int selectedOption = 0;
    public int selectedActiveIndex = 0;
    public int selectedSkillIndex = 0;

    public enum selectionMode { characterSelection, activeSlot, selectableSlot }
    public selectionMode currentSelectionMode = selectionMode.characterSelection;

    public Transform superParentA;
    public Transform superParentB;
    private Transform currentParent;


    Vector2 originalH1;
    Vector2 originalH2;
    RectTransform rectTransformH1;
    RectTransform rectTransformH2;
    void Start()
    {
        rectTransformH1 = activeSlotHighlight.GetComponent<RectTransform>();
        rectTransformH2 = selectedSkillHighlight.GetComponent<RectTransform>();

        originalH1 = rectTransformH1.anchoredPosition;
        originalH2 = rectTransformH2.anchoredPosition;

        activeSlotHighlight.enabled = false;
        selectedSkillHighlight.enabled = false;

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


    public void MoveRight(InputAction.CallbackContext obj)
    {
        /*RectTransform rectTransformH1 = activeSlotHighlight.GetComponent<RectTransform>();
        RectTransform rectTransformH2 = selectedSkillHighlight.GetComponent<RectTransform>();*/

        switch (currentSelectionMode)
        {
            case selectionMode.characterSelection:
                NextOption();
                break;
            case selectionMode.activeSlot: //Changes active slot index and highlighter
                selectedActiveIndex++;
                
                rectTransformH1.anchoredPosition += new Vector2(71f, 0);
                if (rectTransformH1.anchoredPosition == (originalH1 + new Vector2(213f, 0)))
                {
                    rectTransformH1.anchoredPosition = originalH1;
                }
                if (selectedActiveIndex == 3)
                {
                    selectedActiveIndex = 0;
                }
                break;
            case selectionMode.selectableSlot: //Changes selectable slot index and highlighter
                selectedSkillIndex++;
                rectTransformH2.anchoredPosition += new Vector2(71f, 0);
                if (rectTransformH2.anchoredPosition == (originalH2 + new Vector2(355f, 0)))
                {
                    rectTransformH2.anchoredPosition = originalH2;
                }
                if (selectedSkillIndex == 5)
                {
                    selectedSkillIndex = 0;
                }
                break;
        }
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        RectTransform rectTransformH1 = activeSlotHighlight.GetComponent<RectTransform>();
        RectTransform rectTransformH2 = selectedSkillHighlight.GetComponent<RectTransform>();

        switch (currentSelectionMode)
        {
            case selectionMode.characterSelection:
                BackOption();
                break;
            case selectionMode.activeSlot: //Changes active slot index and highlighter
                selectedActiveIndex--;
                rectTransformH1.anchoredPosition += new Vector2(-71f, 0);
                if (rectTransformH1.anchoredPosition == (originalH1 + new Vector2(-71f, 0)))
                {
                    rectTransformH1.anchoredPosition = originalH1 + new Vector2(142f, 0);
                }
                if (selectedActiveIndex == -1)
                {
                    selectedActiveIndex = 2;
                }
                break;
            case selectionMode.selectableSlot: //Changes selectable slot index and highlighter
                selectedSkillIndex--;
                rectTransformH2.anchoredPosition += new Vector2(-71f, 0);
                if (rectTransformH2.anchoredPosition == (originalH2 + new Vector2(-71f, 0)))
                {
                    rectTransformH2.anchoredPosition = originalH2 + new Vector2(284f, 0);
                }
                if (selectedSkillIndex == -1)
                {
                    selectedSkillIndex = 4;
                }
                break;
        }
    }

    public void Confirm(InputAction.CallbackContext obj)
    {
        Child childB = superParentB.GetChild(selectedSkillIndex).GetComponentInChildren<Child>();

        switch (currentSelectionMode)
        {
            case selectionMode.characterSelection:
                currentSelectionMode = selectionMode.activeSlot;
                activeSlotHighlight.enabled = true;
                break;
            case selectionMode.activeSlot:
                currentSelectionMode = selectionMode.selectableSlot;
                selectedSkillHighlight.enabled = true;
                break;
            case selectionMode.selectableSlot:

                returnToParent(selectedActiveIndex);  //If there is a skill in the active slot, it will return it to its original slot **

                if (childB == null) //if there is no skill in the selectable slot, nothing will happen
                {
                    break;
                }

                childB.transform.SetParent(activeSkillSlot[selectedActiveIndex].transform); //if there is a skill in the selectable slot, it will go to the active slot **


                break;
        }
    }

    public void GoBack(InputAction.CallbackContext obj)
    {
        switch (currentSelectionMode)
        {
            case selectionMode.characterSelection:
                break;
            case selectionMode.activeSlot:
                currentSelectionMode = selectionMode.characterSelection;
                activeSlotHighlight.enabled = false;
                break;
            case selectionMode.selectableSlot:
                currentSelectionMode = selectionMode.activeSlot;
                selectedSkillHighlight.enabled = false;
                break;
        }
    }



    //Changing character
    public void NextOption()
    {
        selectedOption++;

        /*if(selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();*/

        if (selectedOption >= characterList.size)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption() 
    {
        selectedOption--;

        /*if (selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();*/

        if (selectedOption < 0)
        {
            selectedOption = characterList.size - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        for (int index = 0; index < activeSkillSlot.Length; index++)
        {
            returnToParent(index);
        }

        //Character character = characterDB.GetCharacter(selectedOption);
        SO_CharacterStat characterStat = characterList.GetCharacterAt(selectedOption);

        //artworkSprite.sprite = character.characterSprite;
        //nameText.text = character.characterName;

        artworkSprite.sprite = characterStat.characterSprite;
        nameText.text = characterStat.characterName;

        for (int i = 0; i < selectableSkillSlot.Length; i++)
        {
            //superParentB.GetChild(i).transform.GetComponentsInChildren<Image>()[1].sprite = character.characterAttacks[i];
            superParentB.GetChild(i).transform.GetComponentsInChildren<Image>()[1].sprite = characterStat.skills[i].skillSprite;
        }
    }



    //Return skill to original slot
    private void returnToParent(int index)
    {
        Child child = activeSkillSlot[index].transform.GetComponentInChildren<Child>();
        if (child != null)
        {
            child.transform.SetParent(child.getOriginalParent());
        }

    }




    //Changing scene and saving player prefs

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
/*
 * 
 * 
 * replace PlayerPref with Scriptable Object
 * Separate highlight mechanic
 * Keep five skill images Static (optional)
 * Use SO_PlayerSelection to save data
 * 
 * Global variables for -> GetComponent
 * Use SO_CharacterStat, SO_SkillStat, SO_CharacterList
 * 
 * 
 * 
 * 
 */


// Note, GameObject that contain these type of functions should have a InputPlayer with the map corresponding to the action

// Template: private void On___  *Name base on action name

//private void OnChangeSelection(InputValue value)
//{
//    print(value.Get<float>());
//}