using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.InputSystem.Interactions;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public class CharacterManager : MonoBehaviour
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


    private int navigationIndex = 0;
    private int selectedOption = 0;
    public int selectedActiveIndex = 0;
    public int selectedSkillIndex = 0;

    public enum selectionMode { characterSelection, activeSlot, selectableSlot }

    public selectionMode currentSelectionMode = selectionMode.characterSelection;



    //TRYING TO SWAP CHILDREN OF PARENTS
    //public Transform parentA;
    //public Transform parentB;
    public Transform superParentA;
    public Transform superParentB;

 

    private Transform currentParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    NextOption();
                    break;
                case selectionMode.activeSlot: //update the index of the selectedActiveIndex
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
                case selectionMode.selectableSlot: //update the index of the selectedSkillIndex
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            //NextOption();
        }



        //Transform childA = superParentA.GetChild(selectedActiveIndex).GetChild(0);
        OriginalSlot childB = superParentB.GetChild(selectedSkillIndex).GetComponentInChildren<OriginalSlot>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (currentSelectionMode)
            {
                case selectionMode.characterSelection:
                    currentSelectionMode = selectionMode.activeSlot;
                    activeSlotHighlight.enabled = true;
                    //clean active slots
                    break;
                case selectionMode.activeSlot:
                    //get the index
                    currentSelectionMode = selectionMode.selectableSlot;
                    selectedSkillHighlight.enabled = true;
                    break;
                case selectionMode.selectableSlot:

                    /*Vector3 childAPosition = childA.localPosition;
                    Vector3 childBPosition = childB.localPosition;

                    childA.SetParent(superParentB.GetChild(selectedSkillIndex));
                    childB.SetParent(superParentA.GetChild(selectedActiveIndex));

                    childA.localPosition = childBPosition;
                    childB.localPosition = childAPosition;*/

                    returnToParent(selectedActiveIndex);

                    if (childB == null)
                    {
                        break;
                    }

                    childB.transform.SetParent(activeSkillSlot[selectedActiveIndex].transform);


                    break;


            }
        }

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

    private void UpdateCharacter(int selectedOption)
    {
        for (int index = 0; index < activeSkillSlot.Length; index++)
        {
            returnToParent(index);
        }

        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;


        //Sprites for skills
        /*for (int i = 0; i < 5; i++)
        {
            currentParent = superParentB.GetChild(i);
            for (int j = 0; j < 5; j++)
            {
                currentParent.GetChild(0).GetComponent<Image>().sprite = character.characterAttacks[i];
            }
        }*/

        for (int i = 0; i < selectableSkillSlot.Length; i++)
        {
            superParentB.GetChild(i).transform.GetComponentsInChildren<Image>()[1].sprite = character.characterAttacks[i];
        }



    }

    private void returnToParent(int index)
    {
        OriginalSlot child = activeSkillSlot[index].transform.GetComponentInChildren<OriginalSlot>();
        if (child != null)
        {
            child.transform.SetParent(child.getOriginalParent());
            //child.resetPosition();
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

    






    private void UpdateSkills(int selectedOption)
    {
        /*for (int i = 0; i < 5; i++)
        {
            superParentB.GetChild(i).GetChild(i).GetComponent<Image> = character.
        }*/
    }
}
