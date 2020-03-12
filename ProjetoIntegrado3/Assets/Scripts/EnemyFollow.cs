using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform Player;
    public Transform[] RandomPoints;
    public float FieldofView = 30;
    public float FollowDistance = 20;
    public float AttackDistance = 2;
    public float PatrolSpeed = 3;
    public float ChasingSpeed = 6;
    public float AttackTime = 1.5f;
    public float Damage = 100;


    private NavMeshAgent NaveMesh;
    private float PlayerDistance;
    private float WayPointDistance;
    private float ChaseChronom;
    private float AttackChronom;
    private int AtualWayPoint;
    private bool VisiblePlayer;
    private bool ChasingSomething;
    private bool ContChasingSomething;
    private bool AttackingSomething;
    
    
    // Start is called before the first frame update
    void Start()
    {
        AtualWayPoint = Random.Range(0, RandomPoints.Length);
        NaveMesh = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDistance = Vector3.Distance(Player.transform.position,transform.position);
        WayPointDistance = Vector3.Distance(RandomPoints[AtualWayPoint].transform.position, transform.position);


        RaycastHit hit;

        Vector3 deOnde = transform.position;
        Vector3 paraOnde = Player.transform.position;
        Vector3 Direction = paraOnde - deOnde;

        if(Physics.Raycast(transform.position,Direction,out hit,1000) && PlayerDistance < FieldofView)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                VisiblePlayer = true;
            }
            else
            {
                VisiblePlayer = false;
            }

        }

        if (PlayerDistance > FieldofView)
        {
            Patrol(); 
        }

        if(PlayerDistance<=FieldofView && PlayerDistance > FollowDistance)
        {
            if (VisiblePlayer == true)
            {
                Look();
            }
            else
            {
                Patrol();
            }
        }
        if(PlayerDistance<=FollowDistance && PlayerDistance > AttackDistance)
        {
            if (VisiblePlayer == true)
            {
                Follow();
                ChasingSomething = true;
            }
            else
            {
                Patrol();
            }
        }
        if (PlayerDistance <= AttackDistance)
        {
            Attack();
        }

        if (WayPointDistance <= 2)
        {
            AtualWayPoint = Random.Range(0, RandomPoints.Length);
            Patrol();
        }

        if (ContChasingSomething == true)
        {
            ChaseChronom += Time.deltaTime;
        }

        if(ChaseChronom>=5 && VisiblePlayer == false)
        {
            ChasingSomething = false;
            ContChasingSomething = false;
            ChaseChronom = 0;
        }

        if (AttackingSomething == true)
        {
            AttackChronom += Time.deltaTime;
        }

        if(AttackChronom>=AttackTime && PlayerDistance <= AttackDistance )
        {
            AttackingSomething = true;
            AttackChronom = 0;
            PlayerLife.Life = PlayerLife.Life - Damage;
            Debug.Log("Recebeu Ataque");
;        }
            else if(AttackChronom >= AttackTime && PlayerDistance > AttackDistance)
            {
            AttackingSomething = false;
            AttackChronom = 0;
            Debug.Log("Errou");
            }
    }

    void Patrol()
    {
        if (ChasingSomething == false)
        {
            NaveMesh.acceleration = 5;
            NaveMesh.speed = PatrolSpeed;
            NaveMesh.destination = RandomPoints[AtualWayPoint].position;
;        }
        else if (ChasingSomething == true)
        {
            ContChasingSomething = true;
        }
    }

    void Look()
    {
        NaveMesh.speed = 0;
        transform.LookAt(Player);
    }

    void Follow()
    {
        NaveMesh.acceleration = 8;
        NaveMesh.speed = ChasingSpeed;
        NaveMesh.destination = Player.position;
    }

    void Attack()
    {
        AttackingSomething = true;
    }
}
