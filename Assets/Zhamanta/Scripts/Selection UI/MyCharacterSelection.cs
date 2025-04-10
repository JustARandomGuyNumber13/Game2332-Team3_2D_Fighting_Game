using UnityEngine;

public class MyCharacterSelection : MonoBehaviour
{
    public int characterSelectionIndex;
    public int skill1Index;
    public int skill2Index;
    public int skill3Index;

    public MyCharacterSelection(int characterSelectionIndex, int skill1Index, int skill2Index, int skill3Index)
    {
        this.characterSelectionIndex = characterSelectionIndex;
        this.skill1Index = skill1Index;
        this.skill2Index = skill2Index;
        this.skill3Index = skill3Index;
    }
}
