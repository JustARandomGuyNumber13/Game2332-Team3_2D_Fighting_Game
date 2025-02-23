using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected SO_Layer _layer;
    [SerializeField] protected Transform _spawnPosition;
    [SerializeField] protected Vector2 _spawnOffset;
    [SerializeField] protected float _damageAmount;
    [SerializeField] protected float _launchSpeed;

    protected PlayerHealthHandler _otherHealthHandler;
    protected PlayerInputHandler _otherInputHandler;
    protected GameObject _shooter;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_layer.playerLayer & (1 << collision.gameObject.layer)) != 0)  // Compare bits (if "name" is in "invite list")
        {
            if (collision.gameObject != _shooter)
            {
                if (_otherHealthHandler == null)
                    _otherHealthHandler = collision.GetComponent<PlayerHealthHandler>();

                if(_otherInputHandler == null)
                    _otherInputHandler = collision.GetComponent<PlayerInputHandler>();

                DealDamageBehavior(collision.gameObject);
            }
        }
        DeactivateProjectile();
    }

    protected virtual void DealDamageBehavior(GameObject otherPlayer) 
    {
        Debug.Log("Hit opponent player", gameObject);
    }

    protected void DeactivateProjectile()
    {
        _rb.linearVelocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
    public virtual void LaunchProjectile(GameObject shooter)
    {
        Vector3 offset = Vector3.right * (_spawnOffset.x * transform.lossyScale.x) + Vector3.up * _spawnOffset.y;
        _shooter = shooter;
        transform.position = _spawnPosition.position + offset;
        this.gameObject.SetActive(true);
        _rb.AddForce(Vector2.right * transform.lossyScale.x * _launchSpeed, ForceMode2D.Impulse);
    }
}
