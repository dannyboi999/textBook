using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 10);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "target") 
		{
			//Destroy (coll.gameObject);
			coll.gameObject.GetComponent<ManageTargetHealth>().gotHit(10);
			GameObject.Find ("player").GetComponent<ManagePlayerHealth2D> ().increaseScore ();
			Destroy (gameObject);
		}
	}

}
