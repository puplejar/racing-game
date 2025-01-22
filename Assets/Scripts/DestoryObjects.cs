using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObjects : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
    }
}
