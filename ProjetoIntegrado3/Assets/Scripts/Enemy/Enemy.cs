using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public Transform eyes;
    public AudioClip[] footsounds;
    public AudioSource growl;

    private NavMeshAgent nav;
    private AudioSource sound;
    private Animator anim;
    private string state = "idle";
    private bool alive = true;
    private bool highAlert = false;
    private float wait = 0f;
    private float alertness = 100;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nav.speed = 1f;
        anim.speed = 1f;
    }

    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    //Checagem para saber se está vendo o player
    public void checkSight()
    {
        if (alive)
        {
            RaycastHit rayHit;
            if(Physics.Linecast(eyes.position,player.transform.position,out rayHit))
            {
                print("hit " + rayHit.collider.gameObject.name);
                if (rayHit.collider.gameObject.name == "Player")
                {
                    if (state != "kill")
                    {
                        state = "chase";
                        nav.speed=2.5f;
                        anim.speed=2.5f;
                        growl.pitch = 1.2f;
                        growl.Play();
                    }
                }


            }
;
        }
    }

    
    void Update()
    {
        if (alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);

            //Idle//

            if (state == "idle")
            {
                //escolher um lugar aleatório para ir
                Vector3 randomPos = Random.insideUnitSphere * alertness;
                NavMeshHit navHit;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 100, NavMesh.AllAreas);

                //fica rodando ao redor do player
                if (highAlert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 40f, NavMesh.AllAreas);

                    //ao passar do tempo ele começa a perder a posição do player

                    alertness += 5f;
                    if (alertness > 40f)
                    {
                        highAlert = false;
                        nav.speed = 1.2f;
                        anim.speed = 1.2f;
                    }
                }

                nav.SetDestination(navHit.position);
                state = "walk";
            }

            //Walk//

            if (state == "walk")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                }
            }
            //Search//

            if (state == "search")
            {
                if (wait >  0f)
                {
                    wait -= Time.deltaTime;
                    transform.Rotate(0f, 30f * Time.deltaTime, 0f);

                }
                else
                {
                    state = "idle";
                }
            }

            //Chase//

            if (state == "chase")
            {
                nav.destination = player.transform.position;

                //perdeu a visão do Player//
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > 10f)
                {
                    state = "hunt";
                }
;           }

            //Hunt//

            if (state == "hunt")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                    highAlert = true;
                    alertness = 5f;
                    checkSight();
                }
            }


            //nav.SetDestination(player.transform.position);
        }

    }

  
}
