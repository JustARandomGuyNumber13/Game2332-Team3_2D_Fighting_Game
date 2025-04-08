using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_CharacterStat", menuName = "Scriptable Objects/SO_CharacterStat")]
public class SO_CharacterStat : ScriptableObject
{
    public string characterName;
    public string characterDescription;
    public AnimationClip characterSprite;
    public GameObject characterPrefab;

    public float moveStandingSpeed;
    public float moveCrouchingSpeed;
    public float jumpForce;
    public int maxHealth;
    public float groundCheckDistance;
    public float defenseValue;

    public SO_SkillStat[] skills;
}
