using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicoptero : MonoBehaviour
{
    private Rigidbody heliRB;
    private void Start()
    {
        heliRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (engineOn)
        {
            AutoForceUp();
            AddForceUp();
        }
    }

    private void FixedUpdate()
    {
        if (engineOn)
        {
            AccellerateBlades();
        }
        else
        {
            DeaccellerateBlades();
        }

        SpinMainBlades();
        SpinBackBlades();
    }

    public bool engineOn = false;
    public void FlipEngine()
    {
        if (engineOn)
        {
            engineOn = false;
        }
        else
        {
            engineOn = true;
        }
    }

    [Header("Gameplay configuration")]
    public float forceUpStrengtht = 5000;
    public void AddForceUp()
    {
        if (Input.GetKey("space"))
        {
            Vector3 force = new Vector3(0, forceUpStrengtht, 0);
            heliRB.AddForce(force, ForceMode.Force);
        }
    }

    [Header("Automation configuration")]
    public float autoForceUpStrenght = 2500;
    public void AutoForceUp()
    {
        Vector3 force = new Vector3(0, autoForceUpStrenght, 0);
        heliRB.AddForce(force, ForceMode.Force);
    }

    [Header("Blades configuration")]
    public GameObject mainBlades;
    public GameObject backBlades;
    public float spinMaxSpeed = 50F;
    public float spinAccelleration = 1F;
    public float spinDeaccelleration = 3F;
    public float currentSpeed = 0;
    public void AccellerateBlades()
    {
        bool notMaxSpeed = currentSpeed < spinMaxSpeed;

        if(notMaxSpeed)
        {
            currentSpeed += spinAccelleration * Time.deltaTime;
        }
    }

    public void DeaccellerateBlades()
    {
        bool stillSpinning = currentSpeed > 0;

        if (stillSpinning)
        {
            currentSpeed -= spinDeaccelleration * Time.deltaTime;
        }

        bool offButNegativeSpin = currentSpeed < 0;

        if (offButNegativeSpin)
        {
            currentSpeed = 0;
        }
    }

    public void SpinMainBlades()
    {
        float speed = currentSpeed * Time.deltaTime;
        Vector3 newRotation = new Vector3(0, currentSpeed, 0);
        mainBlades.transform.Rotate(newRotation);
    }

    public void SpinBackBlades()
    {
        float speed = currentSpeed * Time.deltaTime;
        Vector3 newRotation = new Vector3(currentSpeed, 0, 0);
        backBlades.transform.Rotate(newRotation);
    }
}
