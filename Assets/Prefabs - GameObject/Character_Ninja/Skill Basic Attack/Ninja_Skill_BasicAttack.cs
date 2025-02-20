using UnityEngine;

public class Ninja_Skill_BasicAttack : Skill
{
    [Header("Child class variable")]
    [SerializeField] private SO_Layer _layer;
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
        Debug.Log("Ninja Basic Attack", gameObject);
        RaycastHit2D hit = Physics2D.BoxCast(
            (Vector2) transform.position +  (Vector2.right * transform.localScale.x * _attackOffset.x) + (Vector2.up * _attackOffset.y),
            _attackBoxSize,
            0,
            Vector2.zero,
            0,
            _layer.playerLayer);

        if (hit.collider != null && hit.collider.gameObject != this.gameObject)
        {
            if (_otherPlayerHealthHandler == null)
                _otherPlayerHealthHandler = hit.collider.GetComponent<PlayerHealthHandler>();

            _otherPlayerHealthHandler.DecreaseHealth(_damageAmount);
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    // Get facing direction
    //    float facingDirection = Mathf.Sign(transform.localScale.x);

    //    // Calculate the *flipped* offset
    //    Vector2 flippedOffset = new Vector2(_attackOffset.x * facingDirection, _attackOffset.y);


    //    // Calculate box center
    //    Vector2 boxCenter = (Vector2)transform.position + flippedOffset;

    //    // Calculate half size
    //    Vector2 halfSize = _attackBoxSize * 0.5f;

    //    // Calculate box corners
    //    Vector2 topLeft = boxCenter + new Vector2(-halfSize.x, halfSize.y);
    //    Vector2 topRight = boxCenter + new Vector2(halfSize.x, halfSize.y);
    //    Vector2 bottomLeft = boxCenter + new Vector2(-halfSize.x, -halfSize.y);
    //    Vector2 bottomRight = boxCenter + new Vector2(halfSize.x, -halfSize.y);

    //    // Draw the box
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(topLeft, topRight);
    //    Gizmos.DrawLine(topRight, bottomRight);
    //    Gizmos.DrawLine(bottomRight, bottomLeft);
    //    Gizmos.DrawLine(bottomLeft, topLeft);
    //}
    protected override void AfterSkill() 
    {
        _inputHandler.isCanMove = true;
        _inputHandler.isCanUseSkill = true;
    }
}