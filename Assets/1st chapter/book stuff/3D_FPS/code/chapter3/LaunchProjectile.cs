using UnityEngine;
using System.Collections;

public class LaunchProjectile : MonoBehaviour 
{
	public GameObject projectile;
	public GameObject target;
	float time;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (Input.GetKeyDown (KeyCode.P))
		time +=Time.deltaTime;
		if (time >=2.0) 

		{
			time = 0;
			transform.LookAt(target.transform);
			GameObject t = (GameObject) Instantiate (projectile, transform.position, Quaternion.identity);
			Destroy (t,3);
			transform.LookAt(target.transform);
			//t.GetComponent<Rigidbody>().AddForce(transform.forward * 500);			
			t.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);			
		}

	}
}
