using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool alive = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Eyes")
        {
            other.transform.parent.GetComponent<Enemy>().checkSight();
        }
    }
}
