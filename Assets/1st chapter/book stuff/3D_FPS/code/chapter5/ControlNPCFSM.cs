using UnityEngine;
using System.Collections;

public class ControlNPCFSM : MonoBehaviour 
{

	private float distance;
	private Animator anim;
	private Ray ray;
	private RaycastHit hit;
	private AnimatorStateInfo info; private string objectInSight;
	private GameObject gun;
	public Vector3 direction;
	public bool isInTheFieldOfView;
	public bool noObjectBetweenNPCAndPlayer = false;



	// Use this for initialization 
	void Start ()
	{
		anim = GetComponent<Animator>();
		//gun = GameObject.Find("hand_gun");
		gun = transform.Find("hand_gun").gameObject;
		gun.active = false;

	}
	// Update is called once per frame 
	void Update ()
	{


		direction = (GameObject. Find("FPSController").transform.position - transform.position).normalized;		
		isInTheFieldOfView = (Vector3.Dot(transform.forward.normalized, direction) > .7);
		Debug.DrawRay(transform.position, direction *  100, Color.green);
		Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);	
		if (Physics.Raycast(transform.position, direction * 100, out hit))
		{
			if (hit.collider.gameObject.tag == "Player") noObjectBetweenNPCAndPlayer = true;
			else noObjectBetweenNPCAndPlayer = false;
		}
		if (noObjectBetweenNPCAndPlayer && isInTheFieldOfView)
		{ 
			anim.SetBool ("canSeePlayer", true);
			transform.LookAt(GameObject.Find("playerMiddle").transform);

		}
		else anim.SetBool ("canSeePlayer", false);

		/*if (Input.GetKeyDown (KeyCode.I)) 
		{
			anim.SetBool("canSeePlayer", true); 
		}
		if (Input.GetKeyDown (KeyCode.J)) 
		{
			anim.SetBool("canSeePlayer", false); 
		}	*/	

		//ray.origin = transform.position;

		distance = Vector3.Distance(gameObject.transform.position, GameObject.Find ("FPSController").transform.position);	
		bool withinReach, closeToPlayer;
		withinReach = (distance < 1.5f);
		anim.SetBool("withinArmsReach", withinReach);

		ray.origin = transform.position + Vector3.up;
		ray.direction = transform.forward;
		info = anim.GetCurrentAnimatorStateInfo(0);		
		
		/*
		objectInSight = "";
		Debug.DrawRay (ray.origin, ray.direction * 100, Color.red);		
		if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
		{
			objectInSight = hit.collider.gameObject.tag;
			print ("Object in Sight" + objectInSight);
			if (objectInSight == "Player") 
			{
				anim.SetBool ("canSeePlayer",true);
				print ("Just saw the Player");
			}	
		}
		*/
		if (info.IsName("IDLE")) 
		{
			print("We are in the IDLE state");
			GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
		}
		else if (info.IsName("ATTACK_CLOSE_RANGE"))
		{
			GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
			if (info.normalizedTime%1.0 >= .98)
			{
				GameObject.Find ("FPSController").GetComponent<ManagePlayerHealth>().decreaseHealth(5);
			}	
		}
		else if (info.IsName("HIT"))
		{
			GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
		}
		else if (info.IsName("SHOOT"))
		{
			
			GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
			if(anim.IsInTransition(0)&& anim.GetNextAnimatorStateInfo(0).IsName("FOLLOW_PLAYER")) gun.active = false;
			else gun.active = true;


			transform.LookAt(GameObject.Find("playerMiddle").transform);
			if (info.normalizedTime%1.0 >= .98)
			{
				GameObject.Find ("FPSController").GetComponent<ManagePlayerHealth>().decreaseHealth(5);
			}
		}


		else if (info.IsName("FOLLOW_PLAYER")) 
		{
			//transform.LookAt(GameObject.Find("FPSController").transform); 
			/*transform.LookAt(GameObject.Find("playerMiddle").transform);
			if (objectInSight != "Player")
			{
				anim.SetBool ("canSeePlayer",false); 
				print ("Just lost sight of the Player");
			}
			else 
			{
				transform.Translate(Vector3.forward* Time.deltaTime); print("We are in the FOLLOW_PLAYER state");
			} */

			GetComponent<UnityEngine.AI.NavMeshAgent>().destination = GameObject.Find("playerMiddle").transform.position;
			GetComponent<UnityEngine.AI.NavMeshAgent>().Resume();


		}
	}

	public void setGotHitParameter()
	{
		anim.SetTrigger("gotHit");
	}

	public void setLowHealthParameter()
	{
		anim.SetBool("lowHealth",true);
	}


}
