using UnityEngine;

public class Projectile_FollowCollider : Projectile
{
    [SerializeField] private GameObject thisPlayer;

    protected override void Awake()
    {
        _shooter = thisPlayer;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Global.playerLayerIndex == collision.gameObject.layer)  // Compare bits (if "name" is in "invite list")
        {
            if (collision.gameObject != _shooter)
            {
                if (_otherHealthHandler == null)
                    _otherHealthHandler = collision.GetComponent<PlayerHealthHandler>();

                if (_otherInputHandler == null)
                    _otherInputHandler = collision.GetComponent<PlayerInputHandler>();

                DealDamageBehavior(collision.gameObject);
            }
        }
    }
}
