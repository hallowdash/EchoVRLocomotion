using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderCheck : MonoBehaviour
{
    public bool inCol;
    public SphereCollider sphereCollider;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            inCol=true;
            sphereCollider.radius = 0.2f;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            inCol=false;
            sphereCollider.radius = 0.175f;
        }
    }
}
