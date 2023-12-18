using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManageWeapons2 : MonoBehaviour {




	private const int WEAPON_GUN = 0;
	private const int WEAPON_AUTO_GUN = 1;
	private const int WEAPON_GRENADE = 2;

	private int activeWeapon = WEAPON_GUN;
	private float timer;
	private bool timerStarted;
	private bool canShoot = true;
	private int currentWeapon;

	private bool [] hasWeapon;
	private int [] ammos;
	private int [] maxAmmos;
	private float [] reloadTime;
	private string [] weaponName;	
	private int score;

	// Use this for initialization
	Camera playersCamera;
	Ray rayFromPlayer;
	RaycastHit hit;
	public GameObject sparksAtImpact;
	private int gunAmmo = 10;

	public GameObject grenade;	
	// Use this for initialization
	void Start () 
	{
		ammos = new int [3];
		hasWeapon = new bool [3];
		maxAmmos = new int [3];
		reloadTime = new float [3];	
		weaponName = new string [3];

		hasWeapon [WEAPON_GUN] = true;
		hasWeapon [WEAPON_AUTO_GUN] = true;
		hasWeapon [WEAPON_GRENADE] = true;

		weaponName[WEAPON_GUN] = "GUN";		
		weaponName[WEAPON_AUTO_GUN] = "AUTOMATIC GUN";		
		weaponName[WEAPON_GRENADE] = "GRENADE";		

		ammos [WEAPON_GUN] = 10;
		ammos [WEAPON_AUTO_GUN] = 10;
		ammos [WEAPON_GRENADE] = 10;


		maxAmmos [WEAPON_GUN] = 20;
		maxAmmos [WEAPON_AUTO_GUN] = 20;
		maxAmmos [WEAPON_GRENADE] = 10;

		currentWeapon = WEAPON_GUN;

		playersCamera = GetComponent<Camera>();	
		reloadTime [WEAPON_GUN] = 2.0f;
		reloadTime [WEAPON_AUTO_GUN] = 0.5f;
		reloadTime [WEAPON_GRENADE] = 3.0f;

		score = 0;

	}

	
	// Update is called once per frame
	void Update () 
	{
		if (timerStarted)
		{
			timer += Time.deltaTime;
			if (timer >= reloadTime [currentWeapon])
			{
				timerStarted = false;
				canShoot = true;
			}
		}

		rayFromPlayer = playersCamera.ScreenPointToRay (new Vector3 (Screen.width/2, Screen.height/2, 0));
		Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * 100, Color.red);
		//if (Input.GetKeyDown(KeyCode.F) && gunAmmo>0)
		//if (Input.GetKeyDown(KeyCode.F))
		if (Input.GetKey(KeyCode.F))
		{
			//if (currentWeapon == WEAPON_GUN && ammos [WEAPON_GUN] >=1 && canShoot)
			if ((currentWeapon == WEAPON_GUN  || currentWeapon == WEAPON_AUTO_GUN) && ammos [currentWeapon] >=1 && canShoot)
			{
				ammos [currentWeapon]--;
				GetComponent<AudioSource>().Play();
				if (Physics.Raycast(rayFromPlayer, out hit, 100))
				{
					print (" The object " + hit.collider.gameObject.name +" is in front of the player");
					Vector3 positionOfImpact;
					positionOfImpact = hit.point;
					Instantiate (sparksAtImpact, positionOfImpact, Quaternion.identity);
					GameObject objectTargeted;
					if (hit.collider.gameObject.tag == "target")
					{
						objectTargeted = hit.collider.gameObject;
						//objectTargeted.GetComponent<ManageNPC>().gotHit();
						objectTargeted.GetComponent<ManageNPC2>().gotHit();
					}

				}
				canShoot = false; 
				timer = 0.0f;
				timerStarted = true;

				//gunAmmo --;
				//print ("You have "+gunAmmo + " bullets left");
			}
			if (currentWeapon == WEAPON_GRENADE && ammos [WEAPON_GRENADE] >=1 && canShoot)
			{
				ammos [currentWeapon]--;
				GameObject launcher = GameObject.Find("launcher");
				GameObject grenadeF = (GameObject) (Instantiate (grenade, launcher.transform.position, Quaternion.identity));
				grenadeF.GetComponent<Rigidbody>().AddForce(launcher.transform.forward*200);
				canShoot = false;
				timer = 0.0f;
				timerStarted = true;
			}


		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (hasWeapon[WEAPON_GUN] && hasWeapon[WEAPON_AUTO_GUN] && hasWeapon[WEAPON_GRENADE]) 
			{
				currentWeapon++;
				if (currentWeapon>2) currentWeapon = 0;
			}
			else if (hasWeapon[WEAPON_GUN] && hasWeapon[WEAPON_AUTO_GUN])
			{
				if (currentWeapon == WEAPON_GUN) currentWeapon = WEAPON_AUTO_GUN;
				else currentWeapon = WEAPON_GUN;			
			}
			else if (hasWeapon[WEAPON_GUN] && hasWeapon[WEAPON_GRENADE])
			{
				if (currentWeapon == WEAPON_GUN) currentWeapon = WEAPON_GRENADE;
				else currentWeapon = WEAPON_GUN;				
				}
			else if (hasWeapon[WEAPON_AUTO_GUN] && hasWeapon[WEAPON_GRENADE])
			{
				if (currentWeapon == WEAPON_AUTO_GUN) currentWeapon = WEAPON_GRENADE;
				else currentWeapon = WEAPON_AUTO_GUN;		
			}
			else
			{
			}
			print ("Current Weapon: "+ weaponName[currentWeapon] + "("+ammos[currentWeapon]+")");
		}


		//GameObject.Find("userInfo").GetComponent<Text>().text = weaponName[currentWeapon]+ "("+ammos[currentWeapon]+")";
		if(GameObject.Find("userInfo")!=null) GameObject.Find("userInfo").GetComponent<Text>().text = weaponName[currentWeapon]+ "("+ammos[currentWeapon]+")";

	}

	public void manageCollisions (ControllerColliderHit hit)
	{
		/*print ("Collided with " + hit.collider.gameObject.name);
		if (hit.collider.gameObject.tag =="ammo_gun")
		{
			gunAmmo +=5;
			if (gunAmmo > 10) gunAmmo = 10;
			Destroy (hit.collider.gameObject);
		}*/
		string tagOfTheOtherObject = hit.collider.gameObject.tag;
		if (tagOfTheOtherObject == "ammo_gun" || tagOfTheOtherObject == "ammo_automatic_gun" || tagOfTheOtherObject == "ammo_grenade")
		{
			int indexOfAmmoBeingUpdated = 0;
			if (tagOfTheOtherObject =="ammo_gun") indexOfAmmoBeingUpdated = WEAPON_GUN;
			if (tagOfTheOtherObject =="ammo_automatic_gun") indexOfAmmoBeingUpdated = WEAPON_AUTO_GUN;
			if (tagOfTheOtherObject =="ammo_grenade") indexOfAmmoBeingUpdated = WEAPON_GRENADE;
			ammos [indexOfAmmoBeingUpdated] +=5;
			if (ammos [indexOfAmmoBeingUpdated] > maxAmmos[indexOfAmmoBeingUpdated]) ammos[indexOfAmmoBeingUpdated] = maxAmmos[indexOfAmmoBeingUpdated];
			Destroy (hit.collider.gameObject);
		}			
		if (tagOfTheOtherObject =="health_pack") 
		{
			GameObject.Find ("FPSController").GetComponent<ManagePlayerHealth>().increaseHealth(50);
			Destroy (hit.collider.gameObject);			

		}
		if (tagOfTheOtherObject =="pick_me") 
		{
			Destroy (hit.collider.gameObject);
			score++;
			if (score >=2) Application.LoadLevel("winScene");
		}




	}


}
