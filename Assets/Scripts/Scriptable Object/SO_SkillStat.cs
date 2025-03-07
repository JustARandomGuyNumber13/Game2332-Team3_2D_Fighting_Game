using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_SkillStat", menuName = "Scriptable Objects/SO_SkillStat")]
public class SO_SkillStat : ScriptableObject
{
    public Sprite skillSprite;

    public string skillName;
    public string skillDescription;

    public float skillDelay;
    public float skillDuration;
    public float skillCD;

    public bool isPassiveSkill;

    //public int sfxIndex; //Sound effect index
}
