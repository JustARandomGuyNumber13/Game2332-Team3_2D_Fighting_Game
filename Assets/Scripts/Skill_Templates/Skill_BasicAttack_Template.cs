using UnityEngine;

public class Skill_BasicAttack_Template : Skill
{
    [Header("Skill exclusive variables")]
    [SerializeField] private float _damageAmount;
    [SerializeField] private Vector2 _attackBoxSize;
    [SerializeField] private Vector2 _attackOffset;

    private PlayerHealthHandler _otherPlayerHealthHandler;
    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    protected override void BeforeSkill()
    { 
        _inputHandler.isCanMove = false;
        _inputHandler.isCanUseSkill = false;
    }
    protected override void TriggerSkill()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            (Vector2)transform.position + (Vector2.right * transform.localScale.x * _attackOffset.x) + (Vector2.up * _attackOffset.y),
            _attackBoxSize,
            0,
            Vector2.zero,
            0,
            Global.playerLayer);

        if (hit.collider != null && hit.collider.gameObject != this.gameObject)
        {
            if (_otherPlayerHealthHandler == null)
                _otherPlayerHealthHandler = hit.collider.GetComponent<PlayerHealthHandler>();

            _otherPlayerHealthHandler.Public_DecreaseHealth(_damageAmount);
        }
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}