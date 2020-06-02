using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    float startTime;
    bool isOpened = false;

    
    void OnEnable()
    {
        
        isOpened = !isOpened;

        startTime = Time.time;
    }

    void Update()
    {

        
        if (isOpened)
        {
            
            transform.Rotate(transform.up, -90 * Time.deltaTime);
        }
        else
        {
            
            transform.Rotate(transform.up, 90 * Time.deltaTime);
        }


        
        if (Time.time - startTime > 1f)
        {
            
          enabled = false;
        }
    }
}
