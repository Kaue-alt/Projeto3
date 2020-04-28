using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocaNibuo : MonoBehaviour
{
    public GameObject nibuo;
    public GameObject cameraPlayer;
    public GameObject suportPlayer;
    float playermovement;
    
    public int power = 10;
    bool estaemcena = false;

    GameObject amigo;
    GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invocar();
        Destruir();
    }

    void Invocar()
    {
        if(estaemcena== false && Input.GetKeyDown(KeyCode.G) && power >= 1)
        {
            
                clone = Instantiate(nibuo, new Vector3(transform.position.x+0.5f, transform.position.y+0.3f,transform.position.z+0.5f), Quaternion.Euler(0,90,0)) as GameObject;
                amigo = clone;

                power = power - 1;
                Debug.Log(power);


                Debug.Log("Apareceu");


                estaemcena = true;
                
                
                ThirdPersonCamera cam = cameraPlayer.gameObject.GetComponent<ThirdPersonCamera>();
                cam.target = amigo.transform;
                    
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;

           
            


                 

      
        }

        if (power == 0)
        {
            Debug.Log("Sem Energia");
        }
        

        
    }


    void Destruir()
    {

        if (Input.GetKeyDown(KeyCode.H) && estaemcena==true)
        {
            Debug.Log("Destruir");
            DestroyImmediate(amigo.gameObject);

             ThirdPersonCamera cam = cameraPlayer.gameObject.GetComponent<ThirdPersonCamera>();
             cam.target = suportPlayer.transform;
                    
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;

            estaemcena = false;
        }
    }
}
