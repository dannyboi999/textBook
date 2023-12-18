using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionWithPlayer : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        transform.GetChild(2).GetComponent<ManageWeapons>().manageCollision(hit);
    }
}
