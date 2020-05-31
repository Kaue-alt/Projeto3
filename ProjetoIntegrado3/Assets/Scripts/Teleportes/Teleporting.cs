using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;

    void Update()
    {
        CheckInteracion();

    }
    void OnTriggerEnter(Collider other)
    {

        thePlayer.transform.position = teleportTarget.transform.position; 
    }

    void CheckInteracion()
    {

    }
}
