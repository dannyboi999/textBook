using UnityEngine;
using System.Collections;

public class ManageNPC : MonoBehaviour {

	// Use this for initialization
	private int health;
	public GameObject smoke; 
	void Start () 
	{
		health = 100;
	}
	public void gotHit()
	{
		health -=50;
	}

	
	public void Destroy()
	{
	
		GameObject lastSmoke = Instantiate (smoke, transform.position, Quaternion.identity);
		Destroy (lastSmoke,3);
		Destroy(gameObject);

	}
	void Update () 
	{
		if (health <=0) Destroy();
	}


	public void gotHitByGrenade()
	{
		print ("Hit by grenade");
		health = 0;
	}

}
