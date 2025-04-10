using UnityEngine;

public class SelectedOption : MonoBehaviour
{
    //public CharacterDatabase characterDB;
    public SO_CharactersList characterList;

    public Animator artworkSprite;

    private int selectedOption = 0;


    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption")) //checks if it has a selectedOption saved from last session
        {
            selectedOption = 0; //if it does not, selectedOption is 0
        }

        else
        {
            Load(); //else, it loads saved selectedOtpion
        }

        UpdateCharacter(selectedOption); //shows character based on selectedOption
    }

    private void UpdateCharacter(int selectedOption)
    {
        //Character character = characterDB.GetCharacter(selectedOption); //Creates character; uses GetCharacter function from characterDB based on selectedOption
        SO_CharacterStat characterStat = characterList.GetCharacterAt(selectedOption);

        //artworkSprite.sprite = character.characterSprite;
        //artworkSprite = characterStat.characterSprite;
        artworkSprite.Play(characterStat.characterSprite.name);
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
