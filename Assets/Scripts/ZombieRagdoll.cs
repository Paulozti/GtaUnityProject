using UnityEngine;
using UnityEngine.AI;

public class ZombieRagdoll : MonoBehaviour
{
    [SerializeField] private Collider head, spine, pelvis, rightArm, rightElbow, rightHand, leftArm, leftElbow, leftHand, rightLeg, rightKnee, leftLeg, leftKnee;

    public bool teste;
    public void Awake()
    {
        head.enabled = false;
        spine.enabled = false;
        pelvis.enabled = false;
        rightArm.enabled = false;
        rightElbow.enabled = false;
        rightHand.enabled = false;
        rightLeg.enabled = false;
        rightKnee.enabled = false;
        leftArm.enabled = false;
        leftElbow.enabled = false;
        leftHand.enabled = false;
        leftLeg.enabled = false;
        leftKnee.enabled = false;

    }

    private void Update()
    {
        if (teste)
        {
            ActivateRagdoll();
            teste = false;
        }
            
    }

    public void ActivateRagdoll()
    {
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        head.enabled = true;
        spine.enabled = true;
        pelvis.enabled = true;
        rightArm.enabled = true;
        rightElbow.enabled = true;
        rightHand.enabled = true;
        rightLeg.enabled = true;
        rightKnee.enabled = true;
        leftArm.enabled = true;
        leftElbow.enabled = true;
        leftHand.enabled = true;
        leftLeg.enabled = true;
        leftKnee.enabled = true;

        head.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        spine.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        pelvis.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        rightArm.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        rightElbow.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        rightHand.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        rightLeg.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        rightKnee.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        leftArm.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        leftElbow.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        leftHand.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        leftLeg.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        leftKnee.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(GetComponent<Animator>());

    }
}
