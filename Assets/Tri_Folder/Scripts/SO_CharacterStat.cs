using UnityEngine;

[CreateAssetMenu(fileName = "SO_CharacterStat", menuName = "Scriptable Objects/SO_CharacterStat")]
public class SO_CharacterStat : ScriptableObject
{
    public float moveStandingSpeed;
    public float moveCrouchingSpeed;
    public float jumpForce;
    public int maxHealth;
    public float groundCheckDistance;
    public float defenseValue;
}
