using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float h = 0;
    public float v = 0;

    public float verticalOffset = 30;
    public float horizontalOffset = 30;
    public float maxAngle = 30;
    
 
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        mouseX = Mathf.Clamp(mouseX, 0, Screen.width);

        float mouseY = Input.mousePosition.y;
        mouseY = Mathf.Clamp(mouseY, 0, Screen.height);

        h = mouseX / Screen.width;
        v = -mouseY / Screen.height;


        Vector3 angle = new Vector3(v, h, 0) * maxAngle;

        angle.x += verticalOffset;
        angle.y += horizontalOffset;

        transform.localEulerAngles = angle;
    }
}
