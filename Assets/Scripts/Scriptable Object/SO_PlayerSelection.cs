using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayerSelection", menuName = "Scriptable Objects/SO_PlayerSelection")]
public class SO_PlayerSelection : ScriptableObject
{
    public int CharacterIndex;// { get; private set; }
    public int SkillOneIndex;// { get; private set; }
    public int SkillTwoIndex;// { get; private set; }
    public int SkillThreeIndex;// { get; private set; }

    public void SaveData(int charIndex, int skillOne, int skillTwo, int skillThree)
    {
        CharacterIndex = charIndex;
        SkillOneIndex = skillOne;
        SkillTwoIndex = skillTwo;
        SkillThreeIndex = skillThree;
    }
}
