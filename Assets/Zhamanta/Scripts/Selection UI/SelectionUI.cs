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
    //public CharacterDatabase characterDB;
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

    /*[SerializeField]
    private Image activeSlotHighlight;
    [SerializeField]
    private Image selectedSkillHighlight;*/

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
    [SerializeField]
    Color targetColor1;
    [SerializeField]
    Color targetColor2;
    [SerializeField]
    Color targetColor3;
    [SerializeField]
    float fadeSpeed;
    Color currentTarget;

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
        /*rectTransformH1 = activeSlotHighlight.GetComponent<RectTransform>();
        rectTransformH2 = selectedSkillHighlight.GetComponent<RectTransform>();*/

        /*originalH1 = rectTransformH1.anchoredPosition;
        originalH2 = rectTransformH2.anchoredPosition;*/
        currentTarget = targetColor1;
        fadeImage.enabled = false;

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

    private void Update()
    {
        FadeImageLoop();
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
        fadeImage.enabled = !fadeImage.enabled;

        if (isReady)
        {
            FadeImageLoop();
            Debug.Log("Save data");
            //Debug.Log(playerSkill1 + "" + playerSkill2 + "" + playerSkill3);

            playerSelection.SaveData(selectedOption, playerSkill1, playerSkill2, playerSkill3);
            OnReadyCheck?.Invoke();
        }

        //OnReadyCheck?.Invoke();
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
            OnChangeSelection?.Invoke();
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    NextOption();
                    break;
                case selectionMode.activeSlot: //Changes active slot index and highlighter
                    selectedActiveIndex++;

                    /*rectTransformH1.anchoredPosition += new Vector2(71f, 0);
                    if (rectTransformH1.anchoredPosition == (originalH1 + new Vector2(213f, 0)))
                    {
                        rectTransformH1.anchoredPosition = originalH1;
                    }*/

                    if (selectedActiveIndex == 3)
                    {
                        selectedActiveIndex = 0;
                    }

                    SelectableSlotHighlight(selectedActiveIndex);

                    break;
                case selectionMode.selectableSlot: //Changes selectable slot index and highlighter
                    selectedSkillIndex++;
                    /*rectTransformH2.anchoredPosition += new Vector2(71f, 0);
                    *//*if (rectTransformH2.anchoredPosition == (originalH2 + new Vector2(355f, 0)))
                    {
                        rectTransformH2.anchoredPosition = originalH2;
                    }*/

                    if (selectedSkillIndex == 5)
                    {
                        selectedSkillIndex = 0;
                    }

                    UpdateSkillDescription();
                    SelectedSkillHighlight(selectedSkillIndex);

                    break;
            }
        }   
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        if (!isReady)
        {
            OnChangeSelection?.Invoke();
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    BackOption();
                    break;
                case selectionMode.activeSlot: //Changes active slot index and highlighter
                    selectedActiveIndex--;
                    /*rectTransformH1.anchoredPosition += new Vector2(-71f, 0);
                    if (rectTransformH1.anchoredPosition == (originalH1 + new Vector2(-71f, 0)))
                    {
                        rectTransformH1.anchoredPosition = originalH1 + new Vector2(142f, 0);
                    }*/

                    if (selectedActiveIndex == -1)
                    {
                        selectedActiveIndex = 2;
                    }

                    SelectableSlotHighlight(selectedActiveIndex);

                    break;
                case selectionMode.selectableSlot: //Changes selectable slot index and highlighter
                    selectedSkillIndex--;
                    /*rectTransformH2.anchoredPosition += new Vector2(-71f, 0);
                    if (rectTransformH2.anchoredPosition == (originalH2 + new Vector2(-71f, 0)))
                    {
                        rectTransformH2.anchoredPosition = originalH2 + new Vector2(284f, 0);
                    }*/

                    if (selectedSkillIndex == -1)
                    {
                        selectedSkillIndex = 4;
                    }

                    SelectedSkillHighlight(selectedSkillIndex);
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
                    skillNameText.enabled = false;
                    skillCooldownText.enabled = false;
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
        //Vector2 overallPosition = superParentA.GetComponent<RectTransform>().anchoredPosition;
        Vector2 position = superParentA.GetChild(selectedActiveIndex).GetComponent<RectTransform>().position;
 
        //highlight1Position.anchoredPosition = overallPosition;
        highlight1Position.position = position;
   
   
    }

    private void SelectedSkillHighlight(int selectedSkillIndex)
    {
        Vector2 position = superParentB.GetChild(selectedSkillIndex).GetComponent<RectTransform>().position;
        highlight2Position.position = position;
    }

    private void FadeImageLoop()
    {
        var currentColor = fadeImage.color;
        //var currentTarget = targetColor1;


        if (isReady)
        {

        Debug.Log("Loop Started");
            if (currentTarget == targetColor1)   
            {
                currentColor = Color.Lerp(currentColor, targetColor1, fadeSpeed * Time.deltaTime);
                fadeImage.color = currentColor;
                if (currentColor == targetColor1)
                { 
                    currentTarget = targetColor2;
                }

            }

            if (currentTarget == targetColor2)
            {
                currentColor = Color.Lerp(currentColor, targetColor2, fadeSpeed * Time.deltaTime);
                fadeImage.color = currentColor;
                if (currentColor == targetColor2)
                {
                    currentTarget = targetColor3;
                }

            }

            if (currentTarget == targetColor3)
                {
                    currentColor = Color.Lerp(currentColor, targetColor3, fadeSpeed * Time.deltaTime);
                    fadeImage.color = currentColor;
                    if (currentColor == targetColor3)
                    {
                        currentTarget = targetColor1;
                    }

                }
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
