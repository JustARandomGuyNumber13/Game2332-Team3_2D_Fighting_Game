using UnityEngine;

public class Ninja_Skill_SwordAttack : Skill    // Skill_BasicAttack_Template.cs
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
        _inputHandler.Public_StopMove();
    }
    protected override void TriggerSkill()
    {
        DebugDrawAttackBox();
        RaycastHit2D hit = Physics2D.BoxCast(
            (Vector2) transform.position +  (Vector2.right * transform.localScale.x * _attackOffset.x) + (Vector2.up * _attackOffset.y),
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

    private float debugDuration = 2f;
    private void DebugDrawAttackBox()
    {
        // Get facing direction
        float facingDirection = Mathf.Sign(transform.localScale.x);

        // Calculate the *flipped* offset
        Vector2 flippedOffset = new Vector2(_attackOffset.x * facingDirection, _attackOffset.y);

        // Calculate box center
        Vector2 boxCenter = (Vector3)transform.position + (Vector3)flippedOffset;

        // Calculate half size
        Vector2 halfSize = _attackBoxSize * 0.5f;

        // Calculate box corners
        Vector3 topLeft = boxCenter + new Vector2(-halfSize.x, halfSize.y);
        Vector3 topRight = boxCenter + new Vector2(halfSize.x, halfSize.y);
        Vector3 bottomLeft = boxCenter + new Vector2(-halfSize.x, -halfSize.y);
        Vector3 bottomRight = boxCenter + new Vector2(halfSize.x, -halfSize.y);

        // Draw the box using Debug.DrawLine with 4 parameters
        Debug.DrawLine(topLeft, topRight, Color.red, debugDuration);
        Debug.DrawLine(topRight, bottomRight, Color.red, debugDuration);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, debugDuration);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, debugDuration);
    }
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}