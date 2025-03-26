using UnityEngine;

public class MeteorTrap : Trap
{
    [SerializeField] private GameObject explosionEffect;

    protected override void TrapBehavior()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>(); //Adds gravity to meteor trap
        }

        float randomAngle = Random.Range(-30f, 30f); //Get random angles
        Vector2 velocity = Quaternion.Euler(0, 0, randomAngle) * Vector2.down; //Rotation to a downward vector
        rb.linearVelocity = velocity * Random.Range(3f, 6f); // Randomize speed

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) ;
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity); //Starts explosion effecct
            Deactivate(); //Deactivates gameobject
        }
    }

    public override void Activate()
    {
        transform.position = GetRandomPos();
        Debug.Log($"Meteor spawned at: {transform.position}");
        TrapBehavior();
    }
}
