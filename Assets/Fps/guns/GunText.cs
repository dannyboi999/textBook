using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GunText : MonoBehaviour
{
    public string text;
    public TextMesh textMesh;
    TextMesh thing;
    int count = 0;
    
    public Transform target;
    Vector3 look;
    Vector3 pos;
    private void Update()
    {
        look = transform.position - target.position;
        deleteText();
    }

    public void showText()
    {
        if (ManageWeapons.canSee && count == 0)
        {
            count++;
            textMesh.text = text;
            pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            thing = Instantiate(textMesh, pos, Quaternion.LookRotation(look), transform);
        }
    }

    public void deleteText()
    {
        if(ManageWeapons.canSee == false)
        {
            count = 0;
            Destroy(thing);
        }
    }
}
