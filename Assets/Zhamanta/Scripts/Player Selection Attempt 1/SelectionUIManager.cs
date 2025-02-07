using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.InputSystem.Interactions;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public class SelectionUIManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public Text nameText;
    public SpriteRenderer artworkSprite;
   
    public Image[] activeSkillSlot;
    public Image[] selectableSkillSlot;
    public Image[] skillSprites; 

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

    

    void Start()
    {

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

    private void Update()
    {
        RectTransform rectTransformH1 = activeSlotHighlight.GetComponent<RectTransform>();
        RectTransform rectTransformH2 = selectedSkillHighlight.GetComponent<RectTransform>();

        //Forward key
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    NextOption();
                    break;
                case selectionMode.activeSlot: //Changes active slot index and highlighter
                    selectedActiveIndex++;
                    rectTransformH1.anchoredPosition += new Vector2(83f, 0); 
                    if (rectTransformH1.anchoredPosition.x == 166f)
                    {
                        rectTransformH1.anchoredPosition = new Vector2(-83f, -50f);
                    }
                    if (selectedActiveIndex == 3)
                    {
                        selectedActiveIndex = 0;
                    }
                    break;
                case selectionMode.selectableSlot: //Changes selectable slot index andn highlighter
                    selectedSkillIndex++;
                    rectTransformH2.anchoredPosition += new Vector2(83f, 0);
                    if (rectTransformH2.anchoredPosition.x == 249f)
                    {
                        rectTransformH2.anchoredPosition = new Vector2(-166f, -123);
                    }
                    if (selectedSkillIndex == 5)
                    {
                        selectedSkillIndex = 0;
                    }
                    break; 
            }

               
            //BackOption();
        }

    
        //Confirmation logic
        Child childB = superParentB.GetChild(selectedSkillIndex).GetComponentInChildren<Child>();

        if (Input.GetKeyDown(KeyCode.E))
        {
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

        //Goes back
        if (Input.GetKeyDown(KeyCode.Q))
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

    //Changing character
    private void UpdateCharacter(int selectedOption)
    {
        for (int index = 0; index < activeSkillSlot.Length; index++)
        {
            returnToParent(index);
        }

        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;

        for (int i = 0; i < selectableSkillSlot.Length; i++)
        {
            superParentB.GetChild(i).transform.GetComponentsInChildren<Image>()[1].sprite = character.characterAttacks[i];
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
