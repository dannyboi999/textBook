using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public float grenadeTimer;
    public bool grenadeTimerStarted;
    public float grenadeTimerLimit;
    public bool explode;
    public GameObject explosion;
    private float radius = 2.5f;
    private float power = 500f;
    private float timer;
    private float explosionTime;
    private bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        explosionTime = 2.0f;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= explosionTime && hasExploded == false)
        {
            Vector3 explosionPos = gameObject.transform.position;
            Collider [] colliders = Physics.OverlapSphere(explosionPos, radius);
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.GetComponent<Rigidbody>() != null && colliders[i].gameObject.tag != "Player")
                {
                    GameObject targeted = colliders[i].gameObject;
                    if (targeted.tag == "target" || targeted.tag == "target2")
                    {
                        targeted.GetComponent<manageDamage>().gotHit();
                        print("hit");
                    }
                }
            }
            GameObject Explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(Explosion, 3f);
            hasExploded = true;
            Destroy(gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2.5f);
    }
}

