using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public float chaseDistance = 30f;
    public float stoppingDistance = 5f;
    public float newSpeed = 0.2f;
    public GameObject bullet;
    public bool ZombieIsAlive = true;
    public Animator animator;
    public int bulletShots = 0;
    public Collider zombieCollider;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        SetSpeed(newSpeed);


    }



    void Update()
    {
        if (player != null && ZombieIsAlive)
        {
            float distanceToplayer = Vector3.Distance(transform.position, player.position);

            if (distanceToplayer <= chaseDistance)
            {
                agent.SetDestination(player.position);

                Vector3 directions = player.position - transform.position;
                directions.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(directions);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);


                if (distanceToplayer > agent.stoppingDistance)
                {
                    animator.SetBool("isWalking", true);
                }

                else
                {
                    animator.SetBool("isWalking", false);
                }

                if (distanceToplayer <= agent.stoppingDistance)
                {
                    animator.SetBool("Attack", true);
                }
                else
                {
                    animator.SetBool("Attack", false);
                }


            }
            else if (distanceToplayer > chaseDistance)
            {
                {
                    agent.isStopped = true;
                    animator.SetBool("isWalking", false);
                }

            }


            if(distanceToplayer > 30)
            {
                agent.SetDestination(transform.position);
            }

            SetSpeed(newSpeed);



        }



    }

    void SetSpeed(float speed)
    {
        agent.speed = speed; // Set the speed of the NavMesh Agent
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            bulletShots++;

            if (bulletShots >= 5)
            {
                Debug.Log("Hello");
                animator.SetTrigger("Die");
                agent.isStopped = true;
                zombieCollider.enabled = false;
                agent.enabled = false;
                ZombieIsAlive = false;
                Destroy(this.gameObject,5);

            }
            else
            {
                animator.SetTrigger("Hurt");
            }

            Destroy(collision.gameObject);



        }

    }
}
