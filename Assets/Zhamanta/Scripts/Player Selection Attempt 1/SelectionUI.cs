using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.InputSystem.Interactions;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

public class SelectionUI : MonoBehaviour
{
    //public CharacterDatabase characterDB;
    public SO_CharactersList characterList;

    public TMP_Text nameText;
    public TMP_Text skillDescription;
    public SpriteRenderer artworkSprite;
    
    public Text readyText;
   
    public Image[] activeSkillSlot;
    public Image[] selectableSkillSlot;

    [SerializeField]
    private Image activeSlotHighlight;
    [SerializeField]
    private Image selectedSkillHighlight;

    private int selectedOption = 0; //character selection
    public int selectedActiveIndex = 0; 
    public int selectedSkillIndex = 0; //skill indexes

    public enum selectionMode { characterSelection, activeSlot, selectableSlot }
    public selectionMode currentSelectionMode = selectionMode.characterSelection;

    public Transform superParentA;
    public Transform superParentB;
    private Transform currentParent;


    Vector2 originalH1;
    Vector2 originalH2;
    RectTransform rectTransformH1;
    RectTransform rectTransformH2;

    private int playerSkill1;
    private int playerSkill2;
    private int playerSkill3;

    public UnityEvent<MyCharacterSelection, MyCharacterSelection> OnReady;

    [SerializeField]
    SO_PlayerSelection playerSelection;

    public bool isReady;
    public UnityEvent OnReadyCheck;

    void Start()
    {
        rectTransformH1 = activeSlotHighlight.GetComponent<RectTransform>();
        rectTransformH2 = selectedSkillHighlight.GetComponent<RectTransform>();

        originalH1 = rectTransformH1.anchoredPosition;
        originalH2 = rectTransformH2.anchoredPosition;

        activeSlotHighlight.enabled = false;
        selectedSkillHighlight.enabled = false;

        
        skillDescription.enabled = false;

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

    public void OtherPlayerReadyCheck(SelectionUI otherPlayer)
    {
        if (isReady && (otherPlayer.isReady == true))
        {
            SceneManager.LoadScene(1);
        }
    }
    private void SelfReadyCheck()
    {
        isReady = isReady ? false : true;
        readyText.enabled = !readyText.enabled;

        if (isReady)
        {
            Debug.Log("Save data");
            //Debug.Log(playerSkill1 + "" + playerSkill2 + "" + playerSkill3);

            playerSelection.SaveData(selectedOption, playerSkill1, playerSkill2, playerSkill3);
        }
        OnReadyCheck?.Invoke();
    }

    public void Ready(InputAction.CallbackContext obj)
    {
        if (superParentA.GetChild(0).childCount == 1 && superParentA.GetChild(1).childCount == 1 && superParentA.GetChild(2).childCount == 1)
        {
            getSkillIndex();
            SelfReadyCheck();
        }
        else
        {
            Debug.Log("Cannot be ready. Must have 3 skills selected.");
        }
    }
    public void MoveRight(InputAction.CallbackContext obj)
    {
        if (!isReady)
        {
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

                    UpdateSkillDescription();

                    break;
            }
        }   
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        Debug.Log("Hi");
        if (!isReady)
        {
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

                    UpdateSkillDescription();

                    break;
            }
        }
     
    }

    public void Confirm(InputAction.CallbackContext obj)
    {
        if (!isReady)
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
                    skillDescription.enabled = true;
                    UpdateSkillDescription();
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
    }

    public void GoBack(InputAction.CallbackContext obj)
    {
        if (!isReady)
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
                    skillDescription.enabled = false;
                    break;
            }
        }
    }

    //Changing character
    public void NextOption()
    {
        selectedOption++;

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

        if (selectedOption < 0)
        {
            selectedOption = characterList.size - 1;
        }


        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateSkillDescription()
    {
        SO_CharacterStat characterStat = characterList.GetCharacterAt(selectedOption);
        skillDescription.text = characterStat.skills[selectedSkillIndex].skillDescription;
    }

    private void UpdateCharacter(int selectedOption)
    {
        for (int index = 0; index < activeSkillSlot.Length; index++)
        {
            returnToParent(index);
        }

        SO_CharacterStat characterStat = characterList.GetCharacterAt(selectedOption);

        artworkSprite.sprite = characterStat.characterSprite;
        nameText.text = characterStat.characterName;

        for (int i = 0; i < selectableSkillSlot.Length; i++)
        {
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

    private void getSkillIndex()
    {
        Debug.Log("Test Update");
        int.TryParse(superParentA.GetChild(0).GetChild(0).name, out playerSkill1);
        int.TryParse(superParentA.GetChild(1).GetChild(0).name, out playerSkill2);
        int.TryParse(superParentA.GetChild(2).GetChild(0).name, out playerSkill3);
        Debug.Log(playerSkill1 + "" + playerSkill2 + "" + playerSkill3);
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}


//Make sure to reset selection values when necessary and that saving works the first time ready is clicked and conditions are met
//Make sure loading debug.log actually works, unity event currently not working
