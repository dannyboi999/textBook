using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nameKeeper : MonoBehaviour
{
    public string name;

    // Update is called once per frame
    void Update()
    {
        gameObject.name = name;
    }
}
