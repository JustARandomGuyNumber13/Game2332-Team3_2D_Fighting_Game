using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_SkillStat_Test", menuName = "Scriptable Objects/SO_SkillStat_Test")]
public class SO_SkillStat_Test : ScriptableObject
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
