using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    public Camera cam;
   
    public GameObject enemy;

   

    Text messageText;
    private void Awake()
    {
        cam = Camera.main;

        messageText = GameObject.Find("HUD/MessageText").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
    }

    void CheckInteraction()
    {
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;
        float distance = 4f;

        messageText.text = "";

        RaycastHit hit;

        if(Physics.Raycast(origin,direction,out hit, distance))
        {
            if (hit.transform.tag == "Hideout")
            {
                messageText.text = "Press E to hide";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Enemy stt = enemy.gameObject.GetComponent<Enemy>();
                    stt.state = "hunt";
                    hit.transform.gameObject.GetComponent<Hideouts>().enabled = true;
                    messageText.text = "";
                }
            }

            if (hit.transform.tag == "Teleport")
            {
                messageText.text = "Press E to Teleport";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Enemy stt = enemy.gameObject.GetComponent<Enemy>();
                    stt.state = "hunt";
                    hit.transform.gameObject.GetComponent<Teleport>().enabled = true;
                }
            }

            if (hit.transform.tag == "Door")
            {
                messageText.text = "Press E";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.GetComponent<DoorOpen>().enabled = true;
                }
               
            }

        }
    }
}
      