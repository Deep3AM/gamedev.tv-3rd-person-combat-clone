using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private Collider[] allColliders;
    private Rigidbody[] allRigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        ToggleRagdoll(false);
    }
    public void ToggleRagdoll(bool isRagdoll)//Ragdoll 껏다 켜기
    {
        foreach (Collider collider in allColliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }
        foreach (Rigidbody rigidbody in allRigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;//rigidbody가 kinematic인지 중력의 영향을 받는지
                rigidbody.useGravity = isRagdoll;
            }
        }
        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}
