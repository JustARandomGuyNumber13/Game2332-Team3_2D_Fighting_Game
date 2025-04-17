using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DeathWallTrap : Trap
{
    [SerializeField] private float dmgAmount;
    [SerializeField] private float dmgIncrement;
    [SerializeField] private Vector2 moveSpeed;
    [SerializeField] private float moveDistance;
    [SerializeField] private float pauseDuration;
    [SerializeField] private bool isMoving = true;
    [SerializeField] private float dmgTick;
    private float cloudSpeed;

    private Vector2 lastTargetPos;
    private bool isStarting = true;

    [SerializeField] private UnityEvent OnCollision;
    [SerializeField] private UnityEvent OnActivate;

    private PlayerHealthHandler p1, p2;
    private bool p1Dmg, p2Dmg;
    private bool isDmgOnStay;

    public override void Activate()
    {
        TrapBehavior();
    }
    protected override void TrapBehavior()
    {
        if (!Game_Manager.IsEndGame)
        {
            StartCoroutine(MoveDeathWall());
            StartCoroutine(DamageTickCoroutine());
        }
    }


    private IEnumerator MoveDeathWall()
    {
        while (!Game_Manager.IsEndGame)
        {
            Vector2 startPos = isStarting ? transform.position : lastTargetPos;

            float targetDir = isMoving ? 1 : -1;
            Vector2 targetPos = new Vector2 (startPos.x + (moveDistance * targetDir), transform.position.y);

            lastTargetPos = targetPos;
            isStarting = false;

            while (Vector2.Distance(transform.position, targetPos) >= 0.01f)
            {
                cloudSpeed = Random.Range(moveSpeed.x, moveSpeed.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, cloudSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(pauseDuration);

            dmgAmount += dmgIncrement;
        }
    }
    private IEnumerator DamageTickCoroutine()
    {
        while (!Game_Manager.IsEndGame)
        {
            if(p1Dmg) p1.Public_DecreaseHealth(dmgAmount);
            if(p2Dmg) p2.Public_DecreaseHealth(dmgAmount);
            yield return new WaitForSeconds(dmgTick);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Global.playerLayerIndex)
        {
            if (collision.CompareTag(Global.playerOneTag))
            {
                if (p1 == null)
                    p1 = collision.GetComponent<PlayerHealthHandler>();
                //p1.Public_DecreaseHealth(dmgAmount);
                p1Dmg = true;
            }

            if (collision.CompareTag(Global.playerTwoTag))
            {
                if (p2 == null)
                    p2 = collision.GetComponent<PlayerHealthHandler>();
                //p2.Public_DecreaseHealth(dmgAmount);
                p2Dmg = true;
            }
            OnCollision?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isDmgOnStay) return;

        if (collision.gameObject.layer == Global.playerLayerIndex)
        {
            if (collision.CompareTag(Global.playerOneTag))
            {
                if (p1 == null)
                    p1 = collision.GetComponent<PlayerHealthHandler>();
                p1Dmg = false;
            }

            if (collision.CompareTag(Global.playerTwoTag))
            {
                if (p2 == null)
                    p2 = collision.GetComponent<PlayerHealthHandler>();
                p2Dmg = false;
            }
            OnCollision?.Invoke();
        }
    }
}
