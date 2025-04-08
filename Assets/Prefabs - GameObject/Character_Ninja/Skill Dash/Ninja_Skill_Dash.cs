using UnityEngine;

public class Ninja_Skill_Dash : Skill
{
    [Header("Skill exclusive variables")]
    [SerializeField] private float _dashSpeed;
    
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void BeforeSkill()
    {
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
<<<<<<< HEAD
        _inputHandler.Public_StopMove();
        gameObject.layer = Global.ghostLayerIndex;
=======
        gameObject.layer = Global.ghostLayerIndex;
        Debug.Log("Ninja Dash", gameObject);
>>>>>>> Aaron-Branch
    }
    protected override void DuringSkill(float timer)
    {
        _rb.linearVelocity = Vector2.up * _rb.linearVelocityY + Vector2.right * transform.localScale.x * _dashSpeed;
    }
    protected override void AfterSkill()
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
        gameObject.layer = Global.playerLayerIndex;
    }
}
