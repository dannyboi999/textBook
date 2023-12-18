using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class controlNPCFSM : MonoBehaviour
{
    private Animator animator;
    private Ray ray;
    private RaycastHit hit;
    private AnimatorStateInfo info;
    private string objectinSite;

    public GameObject target;
    public Vector3 pointA;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //keepY();
        Vector3 point = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        ray.origin = point;
        ray.direction = transform.forward;
        info = animator.GetCurrentAnimatorStateInfo(0);
        objectinSite = "";

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if(Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
            pointA = hit.collider.transform.position;
            Vector3 pointB = transform.position;
            Debug.DrawLine(pointA, pointB);

            objectinSite = hit.collider.gameObject.tag;
            print("Object in sight " + objectinSite);
            if(objectinSite == "Player")
            {
                animator.SetBool("canSeePlayer", true);
                print("just saw the player");
            }
        }

        if (info.IsName("IDLE"))
        {
            print("we are idle");
        }
        else if (info.IsName("FOLLOW_PLAYER"))
        {
            transform.LookAt(target.transform);
            if(objectinSite != "Player")
            {
                animator.SetBool("canSeePlayer", false);
                print("cant see player");
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            print("We are following the player");
        }

    }

    //keeps the y position of this thing and doesnt let it change
    //i now realize if u add a hill this breaks
    void keepY()
    {
        Vector3 track = transform.position;

        if (track.y != 1)
        {
            track.y = 1;
        }

        transform.position = track;
    }
}
