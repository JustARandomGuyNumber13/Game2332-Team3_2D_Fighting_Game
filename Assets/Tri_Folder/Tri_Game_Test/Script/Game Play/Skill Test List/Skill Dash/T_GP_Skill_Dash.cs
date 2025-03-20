using UnityEngine;

public class T_GP_Skill_Dash : T_GP_Skill
{
    [Header("Skill exclusive variables")]
    [SerializeField] private float _dashSpeed;
    
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponentInParent<PlayerInputHandler>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    protected override void BeforeSkill()
    {
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
        gameObject.layer = Global.ghostLayerIndex;
    }
    protected override void DuringSkill(float timer)
    {
        _rb.linearVelocity = Vector2.up * _rb.linearVelocityY + Vector2.right * transform.lossyScale.x * _dashSpeed;
        
    }
    protected override void AfterSkill()
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
        gameObject.layer = Global.playerLayerIndex;
    }
}
