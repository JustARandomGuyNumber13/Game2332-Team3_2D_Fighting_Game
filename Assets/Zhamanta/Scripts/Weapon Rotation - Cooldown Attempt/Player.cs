using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public WeaponCycler weaponCycler;

    [SerializeField]
    private GameObject[] _weaponPrefabs;

    public bool _canFire1 = true;
    public bool _canFire2 = true;
    public bool _canFire3 = true;
    public bool _canFire4 = true;


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

        switch (weaponIndex)
        {
            case 0:
                Instantiate(_weaponPrefabs[weaponIndex], transform.position + new Vector3(1.4f, 0, 0), Quaternion.identity);
                break;
            case 1:
                if (_canFire1)
                {
                    Instantiate(_weaponPrefabs[weaponIndex], transform.position + new Vector3(1.4f, 0, 0), Quaternion.identity);
                    _canFire1 = false;
                    Debug.Log("cannot fire");
                }

                StartCoroutine(fire1());

                Countdown countdown1 = GameObject.Find("WeaponCycler").transform.GetComponent<Countdown>();
                StartCoroutine(countdown1.runCountdown(5.0f));

                break;
            case 2:
                if (_canFire2)
                {
                    Instantiate(_weaponPrefabs[weaponIndex], transform.position + new Vector3(1.4f, 0, 0), Quaternion.identity);
                    _canFire2 = false;
                    Debug.Log("cannot fire");
                }

                StartCoroutine(fire2());

                Countdown countdown2 = GameObject.Find("WeaponCycler").transform.GetComponent<Countdown>();
                countdown2.theCountdowns(10.0f);

                break;
            case 3:
                if (_canFire3)
                {
                    Instantiate(_weaponPrefabs[weaponIndex], transform.position + new Vector3(1.4f, 0, 0), Quaternion.identity);
                    _canFire3 = false;
                    Debug.Log("cannot fire");
                }

                StartCoroutine(fire3());

                Countdown countdown3 = GameObject.Find("WeaponCycler").transform.GetComponent<Countdown>();
                countdown3.theCountdowns(15.0f);

                break;
            case 4:
                if (_canFire4)
                {
                    Instantiate(_weaponPrefabs[weaponIndex], transform.position + new Vector3(1.4f, 0, 0), Quaternion.identity);
                    _canFire4 = false;
                    Debug.Log("cannot fire");
                }

                StartCoroutine(fire4());

                Countdown countdown4 = GameObject.Find("WeaponCycler").transform.GetComponent<Countdown>();
                countdown4.theCountdowns(20.0f);

                break;
        }


    }

    IEnumerator fire1()
    {
        yield return new WaitForSeconds(5.0f);
        _canFire1 = true;
        Debug.Log("can fire");
    }
    IEnumerator fire2()
    {
        yield return new WaitForSeconds(10.0f);
        _canFire1 = true;
        Debug.Log("can fire");
    }
    IEnumerator fire3()
    {
        yield return new WaitForSeconds(15.0f);
        _canFire1 = true;
        Debug.Log("can fire");
    }
    IEnumerator fire4()
    {
        yield return new WaitForSeconds(20.0f);
        _canFire1 = true;
        Debug.Log("can fire");
    }
}

