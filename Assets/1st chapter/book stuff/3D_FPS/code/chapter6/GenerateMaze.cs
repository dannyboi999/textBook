using UnityEngine;
using System.Collections;

public class GenerateMaze : MonoBehaviour {

	// Use this for initialization


	public GameObject wall, player, npc_guard;
	private int [,] worldMap = new int [,] 
	{
	{1,1,1,1,1,1,1,1,1,1},
	{1,2,1,0,0,0,0,3,0,1},
	{1,0,1,0,1,0,1,0,0,1},
	{1,0,1,0,0,0,0,0,0,1},
	{1,0,1,1,1,1,0,0,0,1},
	{1,3,0,0,0,3,0,0,0,1},
	{1,0,1,0,1,0,1,1,1,1},
	{1,0,0,1,0,0,0,0,0,1},
	{1,3,1,0,0,0,0,0,3,1},
	{1,1,1,1,1,1,1,1,1,1},
				
	};

	void Start () 
	{

		int i,j;
		for (i = 0; i < 10; i++)
		{
			for (j = 0; j < 10; j++)
			{
				GameObject t;
				if (worldMap [i,j] == 1) t = (GameObject)(Instantiate (wall, new Vector3 (50-i*10, 1.5f, 50-j*10), Quaternion.identity));
				if (worldMap [i,j] == 2)
				{
					t = (GameObject)(Instantiate (player, new Vector3 (50-i*10, 1.5f, 50-j*10), Quaternion.identity));
					t.name = "FPSController";
				} 
				if (worldMap [i,j] == 3) t = (GameObject)(Instantiate (npc_guard, new Vector3 (50-i*10, 0.5f, 50-j*10), Quaternion.identity));				
			}
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
