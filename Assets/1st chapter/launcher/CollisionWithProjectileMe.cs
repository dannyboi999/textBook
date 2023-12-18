using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithProjectileMe : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") Destroy(gameObject, 1);
        //Instantiate(explosion, transform.position, Quaternion.identity); <- if i have an explosion particle i would need this line of code to spawn the explosion animation/ sprite

    }
}
