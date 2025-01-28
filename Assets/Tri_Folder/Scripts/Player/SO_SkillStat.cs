using UnityEngine;

[CreateAssetMenu(fileName = "SO_SkillStat", menuName = "Scriptable Objects/SO_SkillStat")]
public class SO_SkillStat : ScriptableObject
{
    public int skillID;

    public string skillName;
    public string skillDescription;

    public float skillDelay;
    public float skillDuration;
    public float skillCD;

    public bool isPassiveSkill;
}
