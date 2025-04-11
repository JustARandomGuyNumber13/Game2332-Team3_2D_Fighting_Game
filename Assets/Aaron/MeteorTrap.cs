using UnityEngine;
using UnityEngine.Events;

public class MeteorTrap : Trap
{
    [SerializeField] private float xLaunchForceMin;
    [SerializeField] private float xLaunchForceMax;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float dmgAmount;
    [SerializeField] private float deactivateDelay;
    [SerializeField] private float lifeSpan;

    [SerializeField] private UnityEvent OnCollision;
    [SerializeField] private UnityEvent OnActivate;
    private Rigidbody2D rb;

    private PlayerHealthHandler p1, p2;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void TrapBehavior()
    {
        OnActivate?. Invoke();
        Vector2 fallDirection = Vector2.right * GetRandomX();
        rb.AddForce(fallDirection, ForceMode2D.Impulse);
        //RotatePlayer(fallDirection);
        Invoke("Deactivate", lifeSpan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != Global.groundLayerIndex && collision.gameObject.layer != Global.playerLayerIndex)
            return;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        RaycastHit2D[] hitList;
        hitList = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, 0, Global.playerLayer);

        if (hitList.Length != 0)
            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider.CompareTag(Global.playerOneTag))
                { 
                    if(p1 == null) p1 = hit.collider.GetComponent<PlayerHealthHandler>();
                    p1.Public_DecreaseHealth(dmgAmount);
                }
                if (hit.collider.CompareTag(Global.playerTwoTag))
                {
                    if (p2 == null) p2 = hit.collider.GetComponent<PlayerHealthHandler>();
                    p2.Public_DecreaseHealth(dmgAmount);
                }
            }

        // TODO: Play explosion animation and audio from Unity Event
        OnCollision?.Invoke();

        Invoke("Deactivate", deactivateDelay);
    }
    /*void RotatePlayer(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }*/

    private float GetRandomX()
    { 
        return Random.Range(xLaunchForceMin, xLaunchForceMax);
    }
}
