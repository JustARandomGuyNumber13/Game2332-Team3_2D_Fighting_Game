using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayerSelection", menuName = "Scriptable Objects/SO_PlayerSelection")]
public class SO_PlayerSelection : ScriptableObject
{

    #region ~~ Variables ~~
    public int _characterIndex;
    public int _skillOneIndex;
    public int _skillTwoIndex;
    public int _skillThreeIndex;
    #endregion

    public void SaveData(int charIndex, int skillOne, int skillTwo, int skillThree)
    {
        _characterIndex = charIndex;
        _skillOneIndex = skillOne;
        _skillTwoIndex = skillTwo;
        _skillThreeIndex = skillThree;
    }

    #region ~~ Public Getters ~~
    public int GetCharacterIndex() { return _characterIndex; }
    public int GetSkillOneIndex() { return _skillOneIndex; }
    public int GetSkillTwoIndex() { return _skillTwoIndex; }
    public int GetSkillThreeIndex() { return _skillThreeIndex; }
    #endregion
}
