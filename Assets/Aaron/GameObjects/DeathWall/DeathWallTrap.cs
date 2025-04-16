using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DeathWallTrap : Trap
{
    [SerializeField] private float dmgAmount;
    [SerializeField] private float dmgIncrement;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float pauseDuration;
    [SerializeField] private bool isMoving = true;

    private Vector2 lastTargetPos;
    private bool isStarting = true;

    [SerializeField] private UnityEvent OnCollision;
    [SerializeField] private UnityEvent OnActivate;

    private Rigidbody2D rb;
    private PlayerHealthHandler p1, p2;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(MoveDeathWall());
    }


    private IEnumerator MoveDeathWall()
    {
        while (true)
        {
            Vector2 startPos = isStarting ? transform.position : lastTargetPos;

            float targetDir = isMoving ? 1f : -1f;
            Vector2 targetPos = new Vector2 (startPos.x + (moveDistance * targetDir), transform.position.y);

            lastTargetPos = targetPos;
            isStarting = false;

            while (Vector2.Distance(transform.position, targetPos) >= 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                yield return null;
            }

            yield return new WaitForSeconds(pauseDuration);

            dmgAmount += dmgIncrement;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Global.playerLayerIndex)
        {
            if (collision.CompareTag(Global.playerOneTag))
            {
                if (p1 == null)
                {
                    p1 = collision.GetComponent<PlayerHealthHandler>();
                }

                if (p1 != null)
                {
                    p1.Public_DecreaseHealth(dmgAmount);
                }
            }

            if (collision.CompareTag(Global.playerTwoTag))
            {
                if (p2 == null)
                {
                    p2 = collision.GetComponent<PlayerHealthHandler>();
                }

                if (p2 != null)
                {
                    p2.Public_DecreaseHealth(dmgAmount);
                }
            }

            OnCollision?.Invoke();
        }
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != Global.groundLayerIndex && collision.gameObject.layer != Global.playerLayerIndex)
            return;

        RaycastHit2D[] hitList;
        hitList = Physics2D.BoxCast(transform.position, Vector2.zero, 0);

        if (hitList.Length != 0)
            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider.CompareTag(Global.playerOneTag))
                {
                    if (p1 == null) p1 = hit.collider.GetComponent<PlayerHealthHandler>();
                    p1.Public_DecreaseHealth(dmgAmount);
                }
                if (hit.collider.CompareTag(Global.playerTwoTag))
                {
                    if (p2 == null) p2 = hit.collider.GetComponent<PlayerHealthHandler>();
                    p2.Public_DecreaseHealth(dmgAmount);
                }
            }
    }*/
}
