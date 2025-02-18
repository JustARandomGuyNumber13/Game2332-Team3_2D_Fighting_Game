using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected SO_Layer _layer;
    [SerializeField] protected Transform _spawnPosition;
    [SerializeField] protected float _damageAmount;
    [SerializeField] protected float _launchSpeed;

    protected PlayerHealthHandler _otherHealthHandler;
    protected GameObject _shooter;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }
    protected virtual void Start()
    { 
        this.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_layer.playerLayer & (1 << collision.gameObject.layer)) != 0)  // Compare bits (if "name" is in "invite list")
        {
            if (collision.gameObject != _shooter)
            {
                if (_otherHealthHandler == null)
                    _otherHealthHandler = collision.GetComponent<PlayerHealthHandler>();
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
    public void LaunchProjectile(GameObject shooter)
    {
        _shooter = shooter;
        transform.position = _spawnPosition.position;
        this.gameObject.SetActive(true);
        _rb.AddForce(Vector2.right * transform.lossyScale.x * _launchSpeed, ForceMode2D.Impulse);
    }
}
