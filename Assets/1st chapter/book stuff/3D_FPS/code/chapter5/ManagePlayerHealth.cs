﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ManagePlayerHealth : MonoBehaviour 
{
	int health = 100;
	int nbLives = 3;
	public float alpha;
	public bool screenFlashBool;

	// Use this for initialization
	void Start () 
	{
		GameObject [] clones = new GameObject [2];
		clones = GameObject.FindGameObjectsWithTag("Player");
		if (clones.Length > 1) Destroy (clones[1]) ;
		alpha = 0;
		GameObject.Find("screenFlash").GetComponent<Image>().color = new Color (1,0,0,alpha);
		screenFlashBool = false;

	}

	
	// Update is called once per frame
	void Update () 
	{
		print ("Health" + health);
		//GameObject.Find ("healthInfo").GetComponent<Text>().text = "Health: " + health;
		//GameObject.Find ("livesInfo").GetComponent<Text>().text = "Lives: " + nbLives;
		if (GameObject.Find ("healthInfo") !=null) GameObject.Find ("healthInfo").GetComponent<Text>().text = "Health: " + health;
		if (GameObject.Find ("livesInfo") != null) GameObject.Find ("livesInfo").GetComponent<Text>().text = "Lives: " + nbLives;

		if (screenFlashBool)
		{
			alpha -= Time.deltaTime;
			GameObject.Find("screenFlash").GetComponent<Image>().color = new Color (1,0,0,alpha);
			if (alpha <=0) 
			{
				screenFlashBool = false;
				alpha = 0;
			}
		}

	}


	public void decreaseHealth(int healthIncrement)
	{
		print ("Decreasing health by "+ healthIncrement);
		health -= healthIncrement;
		if (health <=0) restartLevel();
		screenFlash();
	}

	public void increaseHealth(int healthIncrement)
	{
		health += healthIncrement;
		if (health > 100) health = 100;
		print ("Increased Health ");
	}

	public void restartLevel()
	{
		nbLives--;
		health = 100;
		if (nbLives >=0) Application.LoadLevel(Application.loadedLevel);
		else Application.LoadLevel("lostScene");


	}
	public void Awake() 
	{
	    DontDestroyOnLoad(transform.gameObject);
	}
	private void screenFlash ()
	{
		screenFlashBool = true;
		alpha = 1.0f;
		print ("Screen Flash");
	}


}
