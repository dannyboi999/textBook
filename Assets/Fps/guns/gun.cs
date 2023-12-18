using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject normalGun;
    public GameObject autoGun;
    public GameObject grenadeLauncher;
    public GameObject test;

    public static List<Weapon> weaponList = new List<Weapon>();

    private void Start()
    {
        weaponList.Add(new Weapon(null, "hands", 0.1f, 1, 10, null));
        weaponList.Add(new Weapon("normalAmmo", "Weapon", 1.0f, 10, 100, normalGun));
        weaponList.Add(new Weapon("autoAmmo", "AutoWeapon", 0.5f, 15, 100, autoGun));
        weaponList.Add(new Weapon("grenade", "GrenadeLauncher", 1.5f, 5, 100, grenadeLauncher));
        weaponList.Add(new Weapon("normalAmmo", "Test", 0.1f, 10, 100, test));
    }
}
