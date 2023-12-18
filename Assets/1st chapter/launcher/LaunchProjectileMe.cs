using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

public class LaunchProjectileMe : MonoBehaviour
{
    public GameObject ball;
    public GameObject target;
    float time;

    // Update is called once per frame
    void Update()
    {
        tracker.pos.y += 3f;
        transform.LookAt(tracker.pos);
        time += Time.deltaTime;
        if (time >= 2.0)
        {
            time = 0;
            GameObject ball = Instantiate(this.ball, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            Destroy(ball, 3);
        }
    }
}
