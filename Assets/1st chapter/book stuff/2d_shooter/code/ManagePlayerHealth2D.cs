using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagePlayerHealth2D : MonoBehaviour {

	// Use this for initialization
	public float timerForShield;
	public bool startInvincibility;
	public int score;
	public AudioClip hitSound;

	void Start () 
	{
		score = 0;
		GameObject.Find ("scoreUI").GetComponent<Text> ().text = "Score:" + score;

		GameObject.Find ("shield").GetComponent<SpriteRenderer> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (startInvincibility) 
		{


			timerForShield += Time.deltaTime;
			if (timerForShield >= 20) 
			{
				timerForShield = 0;
				startInvincibility = false;
				GameObject.Find ("shield").GetComponent<SpriteRenderer> ().enabled = false;
			}



		}



	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if ((coll.gameObject.tag == "target" || coll.gameObject.tag == "bullet") && !startInvincibility) 
		//if (coll.gameObject.tag == "target") 
		{
			Destroy (coll.gameObject);
			DestroyPlayer ();
		}
		if (coll.gameObject.tag == "bonus") 
		{
			Destroy (coll.gameObject);	
			startInvincibility = true;
			GameObject.Find ("shield").GetComponent<SpriteRenderer> ().enabled = true;
		}

	}
	void DestroyPlayer()
	{	
		GetComponent<AudioSource> ().clip = hitSound;
		GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

	}
	public void increaseScore()
	{
		score++;
		GameObject.Find ("scoreUI").GetComponent<Text> ().text = "Score:" + score;
	}




}
