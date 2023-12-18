using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon 
{ 
    public static int normalAmmo = 50;
    public static int autoAmmo = 50;
    public static int grenades = 20;

    public string ammos;
    public string weaponName;
    public float reloadTime;
    public int ammoCollect;
    public int maxAmmos;
    public GameObject gun;

    public Weapon(string ammos, string weaponName, float reloadTime, int ammoCollect, int maxAmmos, GameObject gun)
    {
        this.ammos = ammos;
        this.weaponName = weaponName;
        this.reloadTime = reloadTime;
        this.ammoCollect = ammoCollect;
        this.maxAmmos = maxAmmos;
        this.gun = gun;
    }
}
