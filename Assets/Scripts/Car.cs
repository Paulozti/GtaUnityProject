using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool driving = false;

    [Header("Doors")]
    public CarDoor leftDoor;

    [Header("Wheels")]
    public WheelCollider wheelColliderFL;
    public WheelCollider wheelColliderFR;
    public WheelCollider wheelColliderRL;
    public WheelCollider wheelColliderRR;
    public Transform wheelMeshFL;
    public Transform wheelMeshFR;
    public Transform wheelMeshRL;
    public Transform wheelMeshRR;


    private void FixedUpdate()
    {
        if (driving)
        {
            AccellerateWheel();
            BrakeWheel();
            TurnWheel();
            SpinWheels();
            FixPlayer();

            LeaveCar();
        }
    }

    public Transform driverSeat;
    private void FixPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = driverSeat.position;
        player.transform.rotation = driverSeat.rotation;
    }

    public float currentTorque;
    public float maxTorque;
    public float accelerationTorque;

    public void AccellerateWheel()
    {
        if (Input.GetButton("MoveForward"))
        {
            if(currentTorque < maxTorque) // verifica se esta na velocidade maxima
            {
                currentTorque += accelerationTorque * Time.deltaTime; //adiciona Torque no seu currentTorque
            }
            wheelColliderRL.motorTorque = currentTorque;
            wheelColliderRR.motorTorque = currentTorque;
        }
        else
        {
            currentTorque = 0;
        }
    }

    public float currentbrakeTorque;
    public float maxbrakeTorque;
    public float decelerateTorque;
    public void BrakeWheel()
    {
        if (Input.GetButton("MoveBackward"))
        {
            if (currentbrakeTorque < maxbrakeTorque)
            {
                currentbrakeTorque += decelerateTorque * Time.deltaTime;
            }
            wheelColliderRL.brakeTorque = currentbrakeTorque;
            wheelColliderRR.brakeTorque = currentbrakeTorque;
        }
        else
        {
            currentbrakeTorque = 0;
        }
    }

    public void TurnWheel()
    {
        float xinput = Input.GetAxis("MoveLeft") + Input.GetAxis("MoveRight");
        wheelColliderFR.steerAngle = xinput * 20;
        wheelColliderFL.steerAngle = xinput * 20;
    }

    public void SpinWheels()
    {
        Vector3 position;
        Quaternion rotation;

        wheelColliderFL.GetWorldPose(out position, out rotation);
        wheelMeshFL.position = position;
        wheelMeshFL.rotation = rotation;

        wheelColliderFR.GetWorldPose(out position, out rotation);
        wheelMeshFR.position = position;
        wheelMeshFR.rotation = rotation;

        wheelColliderRL.GetWorldPose(out position, out rotation);
        wheelMeshRL.position = position;
        wheelMeshRL.rotation = rotation;

        wheelColliderRR.GetWorldPose(out position, out rotation);
        wheelMeshRR.position = position;
        wheelMeshRR.rotation = rotation;
    }

    public void LeaveCar()
    {
        if (Input.GetButtonDown("Fire2"))
        { 
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = leftDoor.transform.position;
            player.transform.forward = leftDoor.transform.forward;
            
            Player playerScript = player.GetComponent<Player>();
            CapsuleCollider playerCol = player.GetComponent<CapsuleCollider>();
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            Animator playerAnim = player.GetComponent<Animator>();

            playerCol.enabled = true;
            playerScript.enabled = true;
            playerRB.isKinematic = false;
            playerAnim.SetBool("Driving", false);
            
            driving = false;

            //leftDoor.wantLeftDoorOpen = true;
        }
    }
}
