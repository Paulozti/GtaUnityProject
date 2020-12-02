using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoor : MonoBehaviour
{
    public Car thisCar;

    private void Update()
    {
        if (wantLeftDoorOpen)
        {
            OpenLeftDoor();
        }
        else
        {
            CloseLeftDoor();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool playerwantToOpenDoor = other.gameObject.CompareTag("Player") && Input.GetButtonDown("Fire2");

        if (playerwantToOpenDoor)
        {
            if(wantLeftDoorOpen == false)
            {
                wantLeftDoorOpen = true;
            }
            else
            {
                GetInCar();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wantLeftDoorOpen = false;
        }
    }

    [Header("Door configuration")]
    public Transform leftDoorJoint;
    public Transform respectiveSeat;
    public float maxDoorAngle = 60;
    public float doorOpenSpeed = 120;
    [SerializeField] private float currentDoorAngle = 0;
    public bool wantLeftDoorOpen = false;

    public void OpenLeftDoor()
    {
        if (currentDoorAngle < maxDoorAngle)
        {
            currentDoorAngle += doorOpenSpeed * Time.deltaTime;
        }

        leftDoorJoint.localRotation = Quaternion.Euler(new Vector3(0, currentDoorAngle, 0));
    }

    public void CloseLeftDoor()
    {
        if (currentDoorAngle > 0)
        {
            currentDoorAngle -= doorOpenSpeed * Time.deltaTime;
        }
        else if (currentDoorAngle < 0)
        {
            currentDoorAngle = 0;
        }

        leftDoorJoint.localRotation = Quaternion.Euler(new Vector3(0, currentDoorAngle, 0));
    }

    public void GetInCar()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player playerScript = player.GetComponent<Player>();
        CapsuleCollider playerCol = player.GetComponent<CapsuleCollider>();
        Rigidbody playerRB = player.GetComponent<Rigidbody>();
        Animator playerAnim = player.GetComponent<Animator>();
        Car carSpript = thisCar.GetComponent<Car>();

        playerCol.enabled = false;
        playerScript.enabled = false;
        playerRB.isKinematic = true;
        playerAnim.SetBool("Driving", true);

        player.transform.position = respectiveSeat.position;
        player.transform.forward = respectiveSeat.forward;
        thisCar.driving = true;

        wantLeftDoorOpen = false;
    }

    
}
