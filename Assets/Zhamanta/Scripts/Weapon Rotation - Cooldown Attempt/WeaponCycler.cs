using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;



public class WeaponCycler : MonoBehaviour
{
    [SerializeField]
    private Image[] weaponImages;

    public int currentWeaponIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < weaponImages.Length; i++)
        {
            weaponImages[i].gameObject.SetActive(i == currentWeaponIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleWeapon();
        }
    }

    void CycleWeapon()
    {
        weaponImages[currentWeaponIndex].gameObject.SetActive(false);

        currentWeaponIndex = (currentWeaponIndex + 1) % weaponImages.Length;

        weaponImages[currentWeaponIndex].gameObject.SetActive(true);
    }
}
