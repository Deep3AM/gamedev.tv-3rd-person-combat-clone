using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;
    private Vector3 dampingVelocity;
    private Vector3 impact;//ex)attack move
    private float verticalVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    private void Update()
    {
        if(verticalVelocity<0f&&controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;//normal gravity, not falling
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;//if not ground, falling speed
        }
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
