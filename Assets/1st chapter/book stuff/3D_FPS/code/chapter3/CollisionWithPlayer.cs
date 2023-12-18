using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CollisionWithPlayer : MonoBehaviour {

	// Use this for initialization
	int score;
	void Start () 
	{
		score = 0;
		GameObject.Find("message").GetComponent<Text>().text	="";
	}

	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.gameObject.tag == "pick_me")
		{
			Destroy (collision.collider.gameObject);
			score++;
			print ("Score" +score );
		}
		if (collision.collider.gameObject.name == "end" && score == 4)
		{		
			print("Congratulations!");
			GameObject.Find("message").GetComponent<Text>().text = "Congratulations!";			
		}

	}

}
