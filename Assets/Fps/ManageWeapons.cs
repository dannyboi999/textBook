using UnityEngine;
using UnityEngine.UI;

public class ManageWeapons : MonoBehaviour
{  
    public const int empty = 1;
    public const int GUN_1 = 2;
    public const int GUN_2 = 3;
    public const int GUN_3 = 4;
    public const int GUN_4 = 5;

    private float timer;
    private bool timerStarted;
    private bool canShoot = true;
    public static int currentWeapon;

    public static Weapon[] weaponArray = new Weapon[5];

    Camera playerCam;
    Ray rayFromPlayer;
    public static RaycastHit hit;

    public GameObject sparks;
    public GameObject grenade;

    public GameObject launcher;

    public static bool canSee = false;

    public static string weaponName;

    // Start is called before the first frame update
    void Start()
    {
        weaponArray[0] = gun.weaponList[0];

        currentWeapon = empty;

        playerCam = GetComponent<Camera>();
    }

    public static GameObject PreviousObject = null;
    GameObject current = null;
    // Update is called once per frame
    void Update()
    {
        //draws a ray in the middle of the screen 
        rayFromPlayer = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        //shows the text for the gun
        if (Physics.Raycast(rayFromPlayer, out hit, 100))
        {
            current = hit.transform.gameObject;
            if (hit.transform.gameObject.GetComponent<GunText>() && current != PreviousObject)
            {
                canSee = true;
                weaponName = hit.collider.gameObject.tag;
                hit.transform.gameObject.GetComponent<GunText>().showText();
            }
            else
            {
                current = null;
                canSee = false;
            }
        }
        PreviousObject = current;

        //switches between gun
        switchGun();
        if (currentWeapon > weaponArray.Length)
        {
            currentWeapon = empty;
        }

        if (currentWeapon <= 0)
        {
            currentWeapon = weaponArray.Length;
        }

        //drops the held gun
        dropGun();


        launcher = GameObject.Find("Launcher");

        int number = 0;

        if(weaponArray[currentWeapon - 1].ammos == "autoAmmo")
        {
            number = Weapon.autoAmmo;
        }
        if (weaponArray[currentWeapon - 1].ammos == "normalAmmo")
        {
            number = Weapon.normalAmmo;
        }
        if (weaponArray[currentWeapon - 1].ammos == "grenade")
        {
            number = Weapon.grenades;
        }

        if (weaponArray[currentWeapon - 1].weaponName != "hands")
        {
            GameObject.Find("user info").GetComponent<Text>().text = weaponArray[currentWeapon - 1].weaponName + "(" + number + ")";
        }
        else
        {
            GameObject.Find("user info").GetComponent<Text>().text = "hands";
        }

        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (timer >= weaponArray[currentWeapon - 1].reloadTime)
            {
                timerStarted = false;
                canShoot = true;
                timer = 0.0f;
            }
        }

        shoot();
    }

    public void shoot()
    {
        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * 3, Color.red);
        
        if (weaponArray[currentWeapon - 1].ammos == "grenade" && canShoot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Weapon.grenades--;
            GameObject grenadeF = Instantiate(grenade, launcher.transform.position, Quaternion.identity);
            grenadeF.SetActive(true);
            grenadeF.GetComponent<Rigidbody>().AddForce(launcher.transform.forward * 200);
            canShoot = false;
            timer = 0.0f;
            timerStarted = true;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if ((weaponArray[currentWeapon - 1].ammos == "autoAmmo" || weaponArray[currentWeapon - 1].ammos == "normalAmmo") && canShoot)
            {
                if (weaponArray[currentWeapon - 1].ammos == "normalAmmo")
                {
                    Weapon.normalAmmo--;
                }
                if (weaponArray[currentWeapon - 1].ammos == "autoAmmo")
                {
                    Weapon.autoAmmo--;
                }

                if (currentWeapon != empty)
                    GetComponent<AudioSource>().Play();

                if (Physics.Raycast(rayFromPlayer, out hit, 3) && currentWeapon == empty)
                {
                    Vector3 positionOfImpact = hit.point;
                    GameObject clone = Instantiate(sparks, positionOfImpact, Quaternion.identity);
                    Destroy(clone, 2);
                    GameObject targeted;
                    if (hit.collider.gameObject.tag == "target" || hit.collider.gameObject.tag == "target2")
                    {
                        targeted = hit.collider.gameObject;
                        targeted.GetComponent<manageDamage>().gotHit();
                    }
                }
                else if (Physics.Raycast(rayFromPlayer, out hit, 100) && currentWeapon != empty)
                {
                    //print("the object " + hit.collider.gameObject.name + " is in front of the player");
                    Vector3 positionOfImpact = hit.point;
                    GameObject clone = Instantiate(sparks, positionOfImpact, Quaternion.identity);
                    Destroy(clone, 2);
                    GameObject targeted;
                    if (hit.collider.gameObject.tag == "target" || hit.collider.gameObject.tag == "target2")
                    {
                        targeted = hit.collider.gameObject;
                        targeted.GetComponent<manageDamage>().gotHit();
                    }
                }
                canShoot = false;
                timer = 0.0f;
                timerStarted = true;
            }
        }
    }

    public void manageCollision(ControllerColliderHit yes)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(rayFromPlayer, out hit, 100))
            {
                int index = 0;
                for (int i = 0; i < gun.weaponList.Count; i++)
                {
                    if (hit.collider.gameObject.name == gun.weaponList[i].weaponName)
                    {
                        index = i;
                    }
                }

                int Aindex = 0;
                for (int i = 0; i < weaponArray.Length; i++)
                {
                    if (weaponArray[i] == null)
                    {
                        Aindex = i;
                        break;
                    }
                    else
                    {
                        Aindex = 0;
                    }
                }
                print(Aindex);

                //if you have a weapon it swaps with what u try to pick up 
                if (weaponArray[currentWeapon - 1].gun != null && Aindex == 0 && hit.collider.gameObject.tag == "Weapon")
                {
                    GameObject i = Instantiate(weaponArray[currentWeapon - 1].gun, launcher.transform.position, Quaternion.identity);
                    i.GetComponent<Rigidbody>().AddForce(launcher.transform.forward * 200);
                    weaponArray[currentWeapon - 1] = null;
                    Aindex = currentWeapon - 1;
                }

                if (Aindex > 0 && index != 0)
                {
                    weaponArray[Aindex] = gun.weaponList[index];
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        if(yes.collider.gameObject.tag == "ammo")
        {
            Weapon.normalAmmo += 20;
            Destroy(yes.gameObject);
        }

        if(yes.collider.gameObject.tag == "Auto")
        {
            Weapon.autoAmmo += 30;
            Destroy(yes.gameObject);
        }

        if(yes.collider.gameObject.tag == "grenade")
        {
            Weapon.grenades += 10;
            Destroy(yes.gameObject);
        }
    }

    public void switchGun()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentWeapon > weaponArray.Length)
            {
                currentWeapon = empty;
            }
            else
            {
                currentWeapon++;
            }
            for(int i = 0; i < weaponArray.Length; i++)
            {
                if (currentWeapon > weaponArray.Length)
                {
                    currentWeapon = empty;
                }
                if (weaponArray[currentWeapon - 1] == null)
                {
                    currentWeapon++;
                }
                else
                {
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentWeapon == empty)
            {
                currentWeapon = weaponArray.Length;
            }
            else
            {
                currentWeapon--;
            }
            for (int i = weaponArray.Length - 1; i > 0; i--)
            {
                if (currentWeapon < empty)
                {
                    currentWeapon = weaponArray.Length;
                }
                if (weaponArray[currentWeapon - 1] == null)
                {
                    currentWeapon--;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void dropGun()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (weaponArray[currentWeapon - 1].gun != null)
            {
                GameObject i = Instantiate(weaponArray[currentWeapon - 1].gun, launcher.transform.position, Quaternion.identity);
                i.GetComponent<Rigidbody>().AddForce(launcher.transform.forward * 200);
                weaponArray[currentWeapon - 1] = null;
                currentWeapon = empty;
            }
        }
    }
}
