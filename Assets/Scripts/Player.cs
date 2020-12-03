using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Free,
        Combat,
        Drive,
        Dead
    }

    public State playerState = State.Free;
    public GameObject aim;

    private Vector3 mov;
    private Rigidbody playerRB;
    private Animator playerAnim;
    private GameObject cam;
    public bool playerIsAlive = true;
    public delegate void PlayerIsDead();
    public static event PlayerIsDead OnPlayerDeath;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        cam = Camera.main.gameObject;
        ChangeState();
    }

    private void Update()
    {
        if(playerIsAlive)
            mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void ChangeState()
    {
        StartCoroutine(playerState.ToString());
    }

    IEnumerator Free()
    {
        while(playerState == State.Free && playerIsAlive)
        {
            aim.transform.forward = cam.transform.forward;
            Vector3 cameraRelativeMovement = cam.transform.TransformDirection(mov);
            playerRB.velocity = new Vector3(cameraRelativeMovement.x * 3, playerRB.velocity.y, cameraRelativeMovement.z * 3);
            float dot = Vector3.Dot(transform.forward, -cam.transform.right) * 5;
            transform.Rotate(0, dot, 0);
            Vector3 lockVel = transform.InverseTransformDirection(playerRB.velocity).normalized;

            playerAnim.SetFloat("Walk", lockVel.z);
            playerAnim.SetFloat("SideWalk", lockVel.x + dot);
            playerAnim.SetFloat("Speed", playerRB.velocity.magnitude + Mathf.Abs(dot));    
            yield return new WaitForFixedUpdate();
        }
        ChangeState();
    }

    IEnumerator Combat()
    {
        yield return 10f;

    }

    IEnumerator Drive()
    {
        yield return 10f;
    }

    IEnumerator Dead()
    {
        yield return 10f;
    }

    public void Die()
    {
        playerState = State.Dead;
        playerIsAlive = false;
        OnPlayerDeath?.Invoke();
    }
}
