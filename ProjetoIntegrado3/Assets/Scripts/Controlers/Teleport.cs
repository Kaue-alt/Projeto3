using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player;

    public Transform Target;


    
  
    void Update()
    {
        Player.transform.position = Target.transform.position;
        enabled = false;
    }


}
