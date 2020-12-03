using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GunShoot : MonoBehaviour
{
    Animator playerAnim;
    public VisualEffect vfxmuzzle;
    public VisualEffect vfxricochete;
    public AudioSource source;
    public GameObject gun;
    public Animator gunAnim;
    bool fire;
    public GameObject shootPosition;

    public enum State
    {
        NoWeapon,
        Gun
    }

    public State playerState;
    
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        ChangeState();
    }

    
    void Update()
    {
        fire = Input.GetButtonDown("Fire1");

    }

    void ChangeState()
    {
        StopAllCoroutines();
        StartCoroutine(playerState.ToString());
    }

    void ChangeState(State newState)
    {
        StopAllCoroutines();
        playerState = newState;
        StartCoroutine(playerState.ToString());
    }

    IEnumerator NoWeapon()
    {
        gun.SetActive(false);
        while (playerState == State.NoWeapon)
        {
            yield return new WaitForEndOfFrame();
        }
        ChangeState();
    }
    IEnumerator Gun()
    {
        gun.SetActive(true);
        while(playerState == State.Gun)
        {
            yield return new WaitForEndOfFrame();

            if (!playerAnim.GetBool("Shoot") && fire)
            {
                StartCoroutine(ShootSingle());
            }
        }
        ChangeState();
    }

    IEnumerator ShootSingle()
    {
        playerAnim.SetBool("Shoot", true);
        int force = 10;
        yield return new WaitForSeconds(0.1f);
        vfxmuzzle.Play();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
        Rigidbody rdb = null;
        Vector3 point = Vector3.zero;
        
        if (Physics.Raycast(shootPosition.transform.position, shootPosition.transform.forward, out RaycastHit hit, 100))
        {
            vfxricochete.transform.position = hit.point;
            rdb = hit.collider.GetComponent<Rigidbody>();
            point = hit.point;
            
            if (hit.collider.gameObject.CompareTag("Zombie"))
            {
                Zombie zombie = hit.collider.gameObject.GetComponent<Zombie>();
                force = 250;
                if (zombie != null)
                {
                    zombie.Die();
                }
                else
                {
                    Debug.Log("Não encontrado");
                }
            }
        }

        yield return new WaitForSeconds(0.1f);
        vfxricochete.Play();
        if (rdb)
        {
            if (point.magnitude > 0)
            {
                rdb.AddForceAtPosition(vfxmuzzle.transform.forward * force, point, ForceMode.Impulse);
            }
            else
            {
                rdb.AddForce(vfxmuzzle.transform.forward * force, ForceMode.Impulse);
            }
        }

        yield return new WaitForSeconds(0.1f);
        playerAnim.SetBool("Shoot", false);
    }


}
