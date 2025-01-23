using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public WeaponCycler weaponCycler;

    [SerializeField]
    private GameObject[] _weaponPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(-5, -2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }


    }

    void FireLaser()
    {
        int weaponIndex = weaponCycler.currentWeaponIndex;

        Instantiate(_weaponPrefabs[weaponIndex], transform.position + new Vector3(1.4f, 0, 0), Quaternion.identity);
    }
    
        
}
