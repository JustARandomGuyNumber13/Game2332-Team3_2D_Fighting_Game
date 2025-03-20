using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class T_SM_UI_Skill_Slot : MonoBehaviour
{
    public Image skillImage;
    public Image slotBackgroundImage;
    public TMP_Text text;

    [SerializeField] private Color highlightColor;
    [SerializeField] private Color normalColor;

    public void SetColor(bool isHighlight)
    { 
        slotBackgroundImage.color = isHighlight ? highlightColor : normalColor;
    }
}
