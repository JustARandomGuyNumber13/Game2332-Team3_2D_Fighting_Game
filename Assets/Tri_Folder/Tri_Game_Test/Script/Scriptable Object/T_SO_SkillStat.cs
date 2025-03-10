using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "T_SO_SkillStat", menuName = "Scriptable Objects/T_SO_SkillStat")]
public class T_SO_SkillStat : ScriptableObject
{
    [SerializeField]public GameObject skillPrefab;
    public Sprite skillSprite;

    public string skillName;
    public string skillDescription;
    public float skillDelay;
    public float skillDuration;
    public float skillCD;
    public bool isPassiveSkill;
}
