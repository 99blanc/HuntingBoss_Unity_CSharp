using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();

    void Start()
    {
        if (PlayerPrefs.GetInt("WeaponLoad") == 1)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 2)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 3)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 4)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[3].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 5)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[4].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 6)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[5].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 7)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[6].SetActive(true);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("WeaponLoad") == 1)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 2)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 3)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 4)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[3].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 5)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[4].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 6)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[5].SetActive(true);
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 7)
        {
            for (int i = 0; i < Weapons.Count; ++i)
                Weapons[i].SetActive(false);
            Weapons[6].SetActive(true);
        }
    }
}
