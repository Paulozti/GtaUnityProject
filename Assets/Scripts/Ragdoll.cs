using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody head, spine, pelvis, leftArm, leftElbow, leftHand, leftUpperLeg, leftBottonLeg, leftFoot, rightArm, rightElbow, rightHand, rightUpperLeg, rightBottonLeg, rightFoot;
    private CapsuleCollider capsuleCollider;
    private Rigidbody playerRB;

    public bool Teste = false;
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerRB = GetComponent<Rigidbody>();
        ActiateOrDesactivateBodyColliders();
        DestroyRigidBody();
    }

    // Update is called once per frame
    void Update()
    {
        if (Teste)
        {
            Teste = false;
            capsuleCollider.enabled = false;
            Destroy(playerRB);
            DesactivateKinematic();
            ActiateOrDesactivateBodyColliders();
        }
    }

    void DestroyRigidBody()
    {
        head.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        spine.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        pelvis.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        leftArm.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        leftElbow.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        //leftHand.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        leftUpperLeg.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        leftBottonLeg.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        //leftFoot.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        rightArm.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        rightElbow.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        //rightHand.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        rightUpperLeg.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        rightBottonLeg.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
        //rightFoot.gameObject.GetComponent<Rigidbody>().angularDrag = 0;

    }

    void DesactivateKinematic()
    {
        head.isKinematic = false;
        spine.isKinematic = false;
        pelvis.isKinematic = false;
        leftArm.isKinematic = false;
        leftElbow.isKinematic = false;
        //leftHand.isKinematic = false;
        leftUpperLeg.isKinematic = false;
        leftBottonLeg.isKinematic = false;
        //leftFoot.isKinematic = false;
        rightArm.isKinematic = false;
        rightElbow.isKinematic = false;
        //rightHand.isKinematic = false;
        rightUpperLeg.isKinematic = false;
        rightBottonLeg.isKinematic = false;
        //rightFoot.isKinematic = false;
    }
    void ActiateOrDesactivateBodyColliders() 
    {
        head.gameObject.GetComponent<Collider>().enabled = !head.gameObject.GetComponent<Collider>().enabled;
        spine.gameObject.GetComponent<Collider>().enabled = !spine.gameObject.GetComponent<Collider>().enabled;
        pelvis.gameObject.GetComponent<Collider>().enabled = !pelvis.gameObject.GetComponent<Collider>().enabled;
        leftArm.gameObject.GetComponent<Collider>().enabled = !leftArm.gameObject.GetComponent<Collider>().enabled;
        leftElbow.gameObject.GetComponent<Collider>().enabled = !leftElbow.gameObject.GetComponent<Collider>().enabled;
        //leftHand.gameObject.GetComponent<Collider>().enabled = !leftHand.gameObject.GetComponent<Collider>().enabled;
        leftUpperLeg.gameObject.GetComponent<Collider>().enabled = !leftUpperLeg.gameObject.GetComponent<Collider>().enabled;
        leftBottonLeg.gameObject.GetComponent<Collider>().enabled = !leftBottonLeg.gameObject.GetComponent<Collider>().enabled;
        //leftFoot.gameObject.GetComponent<Collider>().enabled = !leftFoot.gameObject.GetComponent<Collider>().enabled;
        rightArm.gameObject.GetComponent<Collider>().enabled = !rightArm.gameObject.GetComponent<Collider>().enabled;
        rightElbow.gameObject.GetComponent<Collider>().enabled = !rightElbow.gameObject.GetComponent<Collider>().enabled;
        //rightHand.gameObject.GetComponent<Collider>().enabled = !rightHand.gameObject.GetComponent<Collider>().enabled;
        rightUpperLeg.gameObject.GetComponent<Collider>().enabled = !rightUpperLeg.gameObject.GetComponent<Collider>().enabled;
        rightBottonLeg.gameObject.GetComponent<Collider>().enabled = !rightBottonLeg.gameObject.GetComponent<Collider>().enabled;
        //rightFoot.gameObject.GetComponent<Collider>().enabled = !rightFoot.gameObject.GetComponent<Collider>().enabled;
    }
}
