using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SO_AnimatorHash _animatorHash;

    public void Public_MoveAnimation(int moveDirection)
    {
        _animator.SetInteger(_animatorHash.moveDirection, moveDirection);
    }
    public void Public_JumpAnimation(bool isCanJump)
    {
        if (!isCanJump) return;

        _animator.SetTrigger(_animatorHash.jump);
        _animator.SetBool(_animatorHash.isOnGround, false);
    }
    public void Public_VerticalVelocityChange(float yVelocity)
    { 
        _animator.SetFloat(_animatorHash.yVelocity, yVelocity);
    }
    public void Public_LandAnimation()
    {
        _animator.SetBool(_animatorHash.isOnGround, true);
    }
    public void Public_SkillAnimation(int skillIndex)
    {
        _animator.SetTrigger(_animatorHash.useSkill);
        _animator.SetInteger(_animatorHash.skillIndex, skillIndex);
    }
    public void Public_DefendAnimation()
    {
        _animator.SetTrigger(_animatorHash.defend);
    }
    public void Public_HurtAnimation()
    {
        // Implement Hurt animation mechanic
    }
}
