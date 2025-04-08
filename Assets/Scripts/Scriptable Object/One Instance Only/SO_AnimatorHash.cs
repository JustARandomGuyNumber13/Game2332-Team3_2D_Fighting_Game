using UnityEngine;

[CreateAssetMenu(fileName = "SO_AnimatorHash", menuName = "One Instance Only/SO_AnimatorHash")]
public class SO_AnimatorHash : ScriptableObject
{
    /*
     *  This Scriptable Object should only has 1 instance
     */
    [Header("Input string to corresponding Animator's parameters")]
    [Header("Float")]
    [SerializeField] private string _moveDirection;
    [SerializeField] private string _yVelocity;

    [Header("Integer")]
    [SerializeField] private string _skillIndex;

    [Header("Boolean")]
    [SerializeField] private string _isOnGround;
    [SerializeField] private string _isCrouching;
    [SerializeField] private string _defend;

    [Header("Trigger")]
    [SerializeField] private string _useSkill;
    [SerializeField] private string _jump;

    public int moveDirection { get; private set; }
    public int isOnGround { get; private set; }
    public int skillIndex { get; private set; }
    public int useSkill { get; private set; }
    public int isCrouching { get; private set; }
    public int defend { get; private set; }
    public int jump { get; private set; }
    public int yVelocity { get; private set; }

    private void OnValidate()
    {
        moveDirection = Animator.StringToHash(_moveDirection);
        isOnGround = Animator.StringToHash(_isOnGround);
        skillIndex = Animator.StringToHash(_skillIndex);
        useSkill = Animator.StringToHash(_useSkill);
        isCrouching = Animator.StringToHash(_isCrouching);
        defend = Animator.StringToHash(_defend);
        jump = Animator.StringToHash(_jump);
        yVelocity = Animator.StringToHash(_yVelocity);
    }
}
