/***
 * Author: Jacob Sharp
 * Created: 11/14/2022
 * Modified: 
 * Description: Animation controller for NavMeshAgent
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class ChickAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    public float runVelocity = 0.1f;
    public float maxSpeed;
    public string animationRunParameter;
    public string animationSpeedParameter;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        maxSpeed = agent.speed;
    }

    private void Update()
    {
        //animator.SetBool(animationRunParameter, agent.velocity.magnitude > runVelocity);
        animator.SetFloat(animationSpeedParameter, agent.velocity.magnitude / maxSpeed);
    }
}
