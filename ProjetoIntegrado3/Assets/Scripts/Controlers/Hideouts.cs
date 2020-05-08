using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hideouts : MonoBehaviour
{
    public GameObject hideoutcam;
    public GameObject playercam;
    public GameObject player;

    Text messageText;

    bool hidden=false;

    void OnEnable()
    {
        hidden = !hidden;
    }

    void Start()
    {
        messageText = GameObject.Find("HUD/MessageText").GetComponent<Text>();
    }

    
    void Update()
    {
        if (!hidden)
        {
            hideoutcam.gameObject.SetActive(true);
            playercam.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
            messageText.text = "Press E to exit";
            PlayerController alv = player.gameObject.GetComponent<PlayerController>();
            alv.alive = false;
           
            hidden = true;
            return;
           
        }
        
        if(hidden)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                messageText.text = "";
                hideoutcam.gameObject.SetActive(false);
                playercam.gameObject.SetActive(true);
                player.gameObject.SetActive(true);
                enabled = false;
                PlayerController alv = player.gameObject.GetComponent<PlayerController>();
                alv.alive = true;
            }
           
        }
    }

   

    


  
}
