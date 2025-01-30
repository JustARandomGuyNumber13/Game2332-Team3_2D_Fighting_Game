using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(1, 0, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x >= 10)
        {
            Destroy(this.gameObject);
        }
    }
}
