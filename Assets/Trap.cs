using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector3 targetScale;
    public float speed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Damage player (Tri's health function?)
            //Ideally damage over time
        }
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
        if (transform.localScale == targetScale)
        {
            Destroy(gameObject);
        }
    }
}
