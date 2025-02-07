using UnityEngine;

public class Player1 : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public SpriteRenderer artworkSprite;

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
        Character character = characterDB.GetCharacter(selectedOption); //Creates character; uses GetCharacter function from characterDB based on selectedOption
        artworkSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
