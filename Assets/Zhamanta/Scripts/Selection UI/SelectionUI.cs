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
using UnityEditor;

public class SelectionUI : MonoBehaviour
{
    public SO_CharactersList characterList;

    public TMP_Text nameText;
    public TMP_Text skillNameText;
    public TMP_Text skillDescription;
    public TMP_Text skillCooldownText;
    public TMP_Text characterDescription;
    public Animator artworkSprite;
    
    public TMP_Text readyText;
   
    public Image[] activeSkillSlot;
    public Image[] selectableSkillSlot;

    [SerializeField]
    Image activeSlotHighlight;

    [SerializeField]
    Image selectedSkillHighlight;

    [SerializeField]
    RectTransform highlight1Position;

    [SerializeField]
    RectTransform highlight2Position;

    private int selectedOption = 0; //character selection
    public int selectedActiveIndex = 0; 
    public int selectedSkillIndex = 0; //skill indexes

    public enum selectionMode { characterSelection, activeSlot, selectableSlot }
    public selectionMode currentSelectionMode = selectionMode.characterSelection;

    public Transform superParentA;
    public Transform superParentB;
    private Transform currentParent;

    [SerializeField]
    private Image fadeImage;

    private int playerSkill1;
    private int playerSkill2;
    private int playerSkill3;

    public UnityEvent<MyCharacterSelection, MyCharacterSelection> OnReady;

    [SerializeField]
    SO_PlayerSelection playerSelection;

    public bool isReady;
    public UnityEvent OnReadyCheck;
    [SerializeField] private UnityEvent OnChangeSelection;

    void Start()
    {
        fadeImage.gameObject.SetActive(false);

        activeSlotHighlight.enabled = false;
        selectedSkillHighlight.enabled = false;

        
        skillDescription.enabled = false;
        skillNameText.enabled = false;
        skillCooldownText.enabled = false;

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
            SceneManager.LoadScene(Global.gamePlayScene);
        }
    }
    private void SelfReadyCheck()
    {
        isReady = isReady ? false : true;
        readyText.enabled = !readyText.enabled;
        //fadeImage.enabled = !fadeImage.enabled;
        if (!fadeImage.gameObject.activeSelf)
        {
            fadeImage.gameObject.SetActive(true);
        }
        else
        {
            fadeImage.gameObject.SetActive(false);
        }

        if (isReady)
        {
            Debug.Log("Save data");

            playerSelection.SaveData(selectedOption, playerSkill1, playerSkill2, playerSkill3);
            OnReadyCheck?.Invoke();
        }

        //OnReadyCheck?.Invoke();
    }
    
    private void OnSkillThree()
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

    private void OnMove(InputValue value)
    {
        float direction = value.Get<float>();
        ChangeSelections(direction);
    }

    private void ChangeSelections(float direction)
    {
        if (isReady || (direction == 0)) return;

        switch(currentSelectionMode)
        {
            case selectionMode.characterSelection:
                ChangeCharacters(direction);
                break;
            case selectionMode.activeSlot:
                ChooseSlot(direction);
                break;
            case selectionMode.selectableSlot:
                ChooseSkill(direction);
                break;
        }
    }

    private void ChangeCharacters(float direction)
    {
        selectedOption += (int) direction;

        if (selectedOption >= characterList.size)
            selectedOption = 0;

        if (selectedOption < 0)
            selectedOption = characterList.size - 1;

        UpdateCharacter(selectedOption);
        Save();
    }

    private void ChooseSlot(float direction)
    {
        selectedActiveIndex += (int)direction;

        if (selectedActiveIndex == 3)
            selectedActiveIndex = 0;

        if (selectedActiveIndex == -1)
            selectedActiveIndex = 2;

        SelectableSlotHighlight(selectedActiveIndex);
    }

    private void ChooseSkill(float direction)
    {
        selectedSkillIndex += (int) direction;

        if (selectedSkillIndex == 5)
            selectedSkillIndex = 0;

        if (selectedSkillIndex == -1)
            selectedSkillIndex = 4;

        UpdateSkillDescription();
        SelectedSkillHighlight(selectedSkillIndex);
    }

    private void OnSkillTwo() //Confirm
    {
        if (!isReady)
        {
            Child childB = superParentB.GetChild(selectedSkillIndex).GetComponentInChildren<Child>();

            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    currentSelectionMode = selectionMode.activeSlot;
                    activeSlotHighlight.enabled = true;
                    SelectableSlotHighlight(selectedActiveIndex);
                    break;
                case selectionMode.activeSlot:
                    currentSelectionMode = selectionMode.selectableSlot;
                    selectedSkillHighlight.enabled = true;
                    SelectedSkillHighlight(selectedSkillIndex);
                    skillDescription.enabled = true;
                    skillNameText.enabled = true;
                    skillCooldownText.enabled = true;
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

    private void OnSkillOne() //GoBack
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
                    skillNameText.enabled = false;
                    skillCooldownText.enabled = false;
                    break;
            }
        }
    }


    private void UpdateSkillDescription()
    {
        SO_CharacterStat characterStat = characterList.GetCharacterAt(selectedOption);
        skillDescription.text = characterStat.skills[selectedSkillIndex].skillDescription;
        skillNameText.text = characterStat.skills[selectedSkillIndex].skillName;
        skillCooldownText.text = "Cooldown: " + characterStat.skills[selectedSkillIndex].skillCD.ToString() + " s";
    }

    private void UpdateCharacter(int selectedOption)
    {
        for (int index = 0; index < activeSkillSlot.Length; index++)
        {
            returnToParent(index);
        }

        SO_CharacterStat characterStat = characterList.GetCharacterAt(selectedOption);

        Debug.Log(characterStat.characterSprite.name);
        artworkSprite.Play(characterStat.characterSprite.name);
        nameText.text = characterStat.characterName;
        characterDescription.text = characterStat.characterDescription;

        for (int i = 0; i < selectableSkillSlot.Length; i++)
        {
            superParentB.GetChild(i).transform.GetComponentsInChildren<Image>()[1].sprite = characterStat.skills[i].skillSprite;
        }
    }

    private void SelectableSlotHighlight(int selectedActiveIndex)
    {
        Vector2 position = superParentA.GetChild(selectedActiveIndex).GetComponent<RectTransform>().position;
        highlight1Position.position = position;
    }

    private void SelectedSkillHighlight(int selectedSkillIndex)
    {
        Vector2 position = superParentB.GetChild(selectedSkillIndex).GetComponent<RectTransform>().position;
        highlight2Position.position = position;
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
