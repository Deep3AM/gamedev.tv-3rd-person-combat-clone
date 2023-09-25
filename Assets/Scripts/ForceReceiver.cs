using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
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
        if (agent != null && impact.sqrMagnitude <= 0.2f * 0.2f)
        {
            impact = Vector3.zero;//일정 이하로 임팩트가 떨어지 0으로 하여 자연스러운 동작하게함
            agent.enabled = true;
        }
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
        if(agent!=null)
        {
            agent.enabled = false;
        }
    }
    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    internal void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }
}
