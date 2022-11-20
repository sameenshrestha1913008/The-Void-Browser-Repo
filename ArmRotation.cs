using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 90;
    
    // Update is called once per frame
    void Update()
    {
        //Substracting  the position of the player from the mouse position.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();         //Normalizing the Vector. Meaning that all the sum iof the vector will be equal to 1.

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;   //Find the angle in degrees.
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
