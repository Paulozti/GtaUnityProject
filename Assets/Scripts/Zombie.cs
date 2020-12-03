using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    NavMeshAgent nma;
    Transform player;
    public bool canSeePlayer = false;
    public bool haveSeenPlayer = false;
    public bool playerIsNear = false;
    Animator animator;
    public bool isDead = false;
    private ZombieRagdoll zombieRag;
    public bool isAttackingPlayerHp = false;

    public enum ZombieState
    {
        Iddle,
        Attacking
    }
    public ZombieState zombieState;

    public bool teste = false;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        zombieRag = GetComponent<ZombieRagdoll>();
        ChangeZombieState();
    }

    void Update()
    {
        if (!isDead)
        {
            ZombieCanSeePlayer();
        }
        if (teste)
        {
            Die();
            teste = false;
        }
    }

    void ChangeZombieState()
    {
        if (!isDead)
        {
            StopAllCoroutines();
            StartCoroutine(zombieState.ToString());
        }
    }

    IEnumerator Iddle()
    {
        while (!canSeePlayer)
        {
            yield return new WaitForEndOfFrame();
        }
        
        ChangeZombieState();
    }

    IEnumerator Attacking()
    {
        while (canSeePlayer && !isDead)
        {
            haveSeenPlayer = true;
            nma.SetDestination(player.position);
            animator.SetBool("Attacking", true);
            animator.SetFloat("Speed", nma.velocity.magnitude);
            if (nma.remainingDistance < 2)
            {

            }
            yield return new WaitForEndOfFrame();
        }
        ChangeZombieState();
    }

    public void Die()
    {
        isDead = true;
        player.GetComponent<PlayerHP>().ZombieKilled();
        zombieRag.ActivateRagdoll();
    }
    private void ZombieCanSeePlayer()
    {
        Vector3 target = player.position - transform.position;
        float angle = Vector3.Angle(target, transform.forward);
        //Debug.Log(angle);
        if (angle < 70)
        {
            canSeePlayer = true;
            zombieState = ZombieState.Attacking;
        }
        else
        {
            if (haveSeenPlayer && playerIsNear)
            {
                canSeePlayer = true;
                zombieState = ZombieState.Attacking;
            }
            else
            {
                canSeePlayer = false;
                zombieState = ZombieState.Iddle;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
