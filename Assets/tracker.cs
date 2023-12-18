using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracker : MonoBehaviour
{
    public static Vector3 pos;

    public void Update()
    {
        pos = transform.position;
    }
}
