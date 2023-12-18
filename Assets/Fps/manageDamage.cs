using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageDamage : MonoBehaviour
{
    public int Health;
    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "target")
        {
            Health = 100;
        }
        else if(gameObject.tag == "target2")
        {
            Health = 150;
        }
    }

    public void Update()
    {
        if(Health <= 0)
        {
            Destroy();
        }
    }

    public void gotHit()
    {
        if (ManageWeapons.currentWeapon == ManageWeapons.GUN_1)
        {
            Health -= 50;
        }
        if (ManageWeapons.currentWeapon == ManageWeapons.GUN_2)
        {
            Health -= 30;
        }
        if (ManageWeapons.currentWeapon == ManageWeapons.GUN_3)
        {
            Health -= 80;
        }
        if(ManageWeapons.currentWeapon == ManageWeapons.empty)
        {
            Health -= 5;
        }
    }

    public void Destroy()
    {
        GameObject lastSmoke = Instantiate(smoke, transform.position, Quaternion.identity);
        Destroy(lastSmoke, 3);
        Destroy(gameObject);
    }
}
