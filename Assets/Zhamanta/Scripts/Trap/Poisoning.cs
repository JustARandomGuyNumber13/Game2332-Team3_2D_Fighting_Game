using System.Collections;
using UnityEngine;

public class Poisoning : Trap
{
    [SerializeField] Vector3 targetScale;
    [SerializeField] float speed = 5f;
    [SerializeField] float lifeSpan = 5f;
    private PlayerHealthHandler p1, p2;

    [SerializeField] float dmgAmount, dmgDuration, dmgTickDuration;

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Global.playerLayerIndex)
        {
            if (other.gameObject.tag == Global.playerOneTag)
            {
                if (p1 == null) p1 = other.GetComponent<PlayerHealthHandler>();
                p1.Public_DecreaseHealthOverTime(dmgAmount, dmgDuration, dmgTickDuration);
            }

            if (other.gameObject.tag == Global.playerTwoTag)
            {
                if (p2 == null) p2 = other.GetComponent<PlayerHealthHandler>();
                p2.Public_DecreaseHealthOverTime(dmgAmount, dmgDuration, dmgTickDuration);
            }
        }
    }

   

    protected override void TrapBehavior()
    {
        Invoke("Deactivate", lifeSpan);
    }

}
