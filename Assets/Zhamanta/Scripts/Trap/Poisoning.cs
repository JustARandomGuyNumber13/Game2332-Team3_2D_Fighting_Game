using System.Collections;
using UnityEngine;

public class Poisoning : TrapParent
{
    [SerializeField] Vector3 targetScale;
    [SerializeField] float speed = 5f;
    [SerializeField] float lifespam = 5f;
    private PlayerHealthHandler p1, p2;

    [SerializeField] float dmgAmount, dmgDuration, dmgTickDuration;

    private void Start()
    {
        
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
        /*if (transform.localScale == targetScale)
        {

            Deactivate();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Test 1" + other.gameObject.name);

        if (other.gameObject.layer == Global.playerLayerIndex)
        {
            Debug.Log("Test 2" + other.gameObject.name);
            //if (other.gameObject.tag == Global.PlayerOneTag)
            if (other.gameObject.tag == "Player1")
            {
                Debug.Log("Test 3" + other.gameObject.name);
                if (p1 == null) p1 = other.GetComponent<PlayerHealthHandler>();
                Debug.Log(p1 == null);
                p1.Public_DecreaseHealthOverTime(dmgAmount, dmgDuration, dmgTickDuration);
            }

            if (other.gameObject.tag == "Player2")
            {
                Debug.Log("Test 4" + other.gameObject.name);
                if (p2 == null) p2 = other.GetComponent<PlayerHealthHandler>();
                p2.Public_DecreaseHealthOverTime(dmgAmount, dmgDuration, dmgTickDuration);
            }
        }
    }

   

    protected override void TrapBehavior()
    {
        Invoke("Deactivate", lifespam);
    }

}
