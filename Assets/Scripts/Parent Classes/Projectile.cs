using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected SO_Layer _layer;
    [SerializeField] protected Vector2 _spawnOffset;
    [SerializeField] protected float _damageAmount;
    [SerializeField] protected float _launchSpeed;

    protected Transform _spawnPosition;
    protected PlayerHealthHandler _otherHealthHandler;
    protected PlayerInputHandler _otherInputHandler;
    protected GameObject _shooter;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        Transform projectileSpawnPos = transform.parent.parent.Find("ProjectileShootPoint");
        _spawnPosition = projectileSpawnPos;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_layer.playerLayerIndex == collision.gameObject.layer)  // Compare bits (if "name" is in "invite list")
        {
            if (collision.gameObject != _shooter)
            {
                if (_otherHealthHandler == null)
                    _otherHealthHandler = collision.GetComponent<PlayerHealthHandler>();

                if(_otherInputHandler == null)
                    _otherInputHandler = collision.GetComponent<PlayerInputHandler>();

                DealDamageBehavior(collision.gameObject);
                DeactivateProjectile();
            }
        }
        else
            DeactivateProjectile();
    }

    protected virtual void DealDamageBehavior(GameObject otherPlayer) 
    {
        Debug.Log("Hit opponent player " + otherPlayer.name, gameObject);
    }

    public void DeactivateProjectile()
    {
        _rb.linearVelocity = Vector2.zero;
        this.gameObject.SetActive(false);
    }
    public virtual void LaunchProjectile(GameObject shooter)
    {
        this.gameObject.SetActive(true);
        Vector3 offset = Vector3.right * (_spawnOffset.x * transform.lossyScale.x) + Vector3.up * _spawnOffset.y;
        _shooter = shooter;
        transform.position = _spawnPosition.position + offset;
        _rb.AddForce(Vector2.right * transform.lossyScale.x * _launchSpeed, ForceMode2D.Impulse);
    }
}
