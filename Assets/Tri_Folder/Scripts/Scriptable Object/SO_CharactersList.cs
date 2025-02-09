using UnityEngine;

[CreateAssetMenu(fileName = "SO_CharactersList", menuName = "Scriptable Objects/SO_CharactersList")]
public class SO_CharactersList : ScriptableObject
{
    [SerializeField] private SO_CharacterStat[] characters;

    public SO_CharacterStat GetCharacterAt(int index)
    {
        return characters[index];
    }
    public int size
    {get { return characters.Length; } }
}
