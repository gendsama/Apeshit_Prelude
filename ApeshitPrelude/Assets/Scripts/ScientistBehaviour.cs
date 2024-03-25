using GorillaLocomotion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ScientistBehaviour : MonoBehaviour
{

    public Transform[] targetPoint;
    public float maxDetectionRange = 15f; // Maximum detection range

    private bool playerInSight; // Flag to indicate if the player is in sight
    public int currentPoint;

    public NavMeshAgent agent;
    public Animator Animator;

    public float walkSpeed = 1f;
    public float runSpeed = 2f;

    public float waitAtPoint = 5f;
    [SerializeField] private float waitCounter;

    public GameObject explosionParticlePrefab;
    public AudioClip explosionSound;
    public AudioClip explosionDeadSound;
    bool hasExploded = false;
    GameObject explosionInstance;
    private AudioSource audioSource;
    public AudioClip shout;
    bool hasPlayedShout = false;

    public enum AIState
    {
        isDead, isSeekTargetPoint, isSeekPlayer, isAttack
    }
    public AIState state;

    // Start is called before the first frame update
    void Start()
    {

        waitCounter = waitAtPoint;
        // Get the Audio Source component attached to the Scientist
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player._instance.transform.position);
        Vector3 playerPosition = FindPlayerPosition();

        //State switching
        if (!Player._instance.isDead) //if player is not dead
        {
            if (distanceToPlayer < 5f && !hasPlayedShout)
            {
                state = AIState.isSeekPlayer;
                // Debug.Log("isSeekPlayer");
                Animator.SetBool("Run", true);
                audioSource.PlayOneShot(shout);
                hasPlayedShout = true; // Set the flag to true to indicate the sound has been played
            }
            else if (distanceToPlayer > 5f)
            {
                state = AIState.isSeekTargetPoint;
                //Debug.Log("isSeekTargetPoint");
                Animator.SetBool("Walk", true);
            }

            if (distanceToPlayer < 1f)
            {
                state = AIState.isAttack;
                //Debug.Log("isAttack");
            }
        }
        else //player's dead
        {
            state = AIState.isSeekTargetPoint;
        }

        // Set speed based on state
        float speed = state == AIState.isSeekPlayer ? runSpeed : walkSpeed;
        agent.speed = speed;

        switch (state)
        {
            case AIState.isDead:
                Animator.ResetTrigger("Attack");
                Animator.SetTrigger("Dead");
                break;
            case AIState.isSeekPlayer:
                Animator.ResetTrigger("Attack");
                Animator.SetBool("Walk", false);
                Animator.SetBool("Run", true);
                agent.SetDestination(playerPosition);
                break;
            case AIState.isSeekTargetPoint:
                Animator.SetBool("Run", false);
                Animator.ResetTrigger("Attack");
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                        Animator.SetBool("Walk", false);
                        Animator.SetBool("Run", false);
                    }
                    else
                    {
                        SetDestinationToNextPoint();
                        Animator.SetBool("Walk", true);
                        Animator.SetBool("Run", false);
                    }
                }
                break;
            case AIState.isAttack:
                RotateTowardsPlayer(playerPosition);
                agent.stoppingDistance = 0.5f;
                Animator.SetBool("Run", false);
                Animator.SetBool("Walk", false);
                Animator.SetTrigger("Attack");
                break;
        }

    }

    public void EnterAttackAnimation() //Make enemy stop sliding during attack animation.
    {
        agent.isStopped = true;
    }


    public void OnAttackAnimationEnd() //unfreeze the enemy after the attack animation end.
    {
        agent.isStopped = false;
    }

    void SetDestinationToNextPoint() //Randomly set a point for enemy pathfinding.
    {
        // Generate a random point on the NavMesh around the enemy
        Vector3 randomPoint = RandomNavmeshLocation(transform.position, 7f);
        waitCounter = waitAtPoint;
        // Set the destination for the NavMeshAgent
        agent.SetDestination(randomPoint);
    }

    Vector3 RandomNavmeshLocation(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas);

        return navHit.position;
    }


    void RotateTowardsPlayer(Vector3 playerPosition) //make enemy rotates towards player 
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    void Dead()
    {
        // Check if the enemy has already exploded
        if (!hasExploded)
        {
            // Set the flag to true to indicate that the enemy has exploded
            hasExploded = true;

            // Stop agent from moving
            Animator.SetTrigger("Dead");
            agent.isStopped = true;

            // Stop animations
            Animator.ResetTrigger("Attack");
            Animator.SetBool("Walk", false);
            Animator.SetBool("Run", false);

            // Instantiate explosion particle effect at the enemy's position
            explosionInstance = Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

            // Play explosion sound
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            audioSource.PlayOneShot(explosionDeadSound);

            // Destroy the explosion particle effect after it finishes playing
            Destroy(explosionInstance, explosionSound.length);

            // Destroy the gameObject after 3 seconds
            Destroy(gameObject, 3f);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the 'can' tag
        if (other.CompareTag("can"))
        {
            //Debug.Log("Enemy collided with object with 'can' tag.");
            // Call the Dead function to kill the enemy
            Dead();
        }
    }

    Vector3 FindPlayerPosition()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            // Assuming there's only one player, you can return its position
            return players[0].transform.position;
        }
        else
        {
            // No player found, return a default position
            return Vector3.zero;
        }
    }
}

