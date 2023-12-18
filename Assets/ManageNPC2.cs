using UnityEngine;
using System.Collections;

public class ManageNPC2 : MonoBehaviour {

	// Use this for initialization
	private int health;
	public GameObject smoke; 
	void Start () 
	{
		health = 100;
	}

	
	public void Destroy()
	{
	
			/*print ("Destroying "+ gameObject.name);
	GameObject lastSmoke = (GameObject) (Instantiate (smoke, transform.position, Quaternion.identity));
	Destroy (lastSmoke,3);
	Destroy(gameObject);*/
		GetComponent<ControlNPCFSM>().setLowHealthParameter();
		Destroy(gameObject, 5);


	}
	void Update () 
	{
		if (health <=0) Destroy();
	}


	public void gotHit()
	{
		print ("Got hit by bullet");
		GetComponent<ControlNPCFSM>().setGotHitParameter();
		health -=50;
	}
	public void gotHitByGrenade()
	{
		print ("Got hit by Grenade");
		GetComponent<ControlNPCFSM>().setGotHitParameter();
		health = 0;
	}


}
