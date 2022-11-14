/***
 * Author: Jacob Sharp
 * Created: 11/14/2022
 * Modified:
 * Description: Move to destination
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowDestination : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform destination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(destination.position);
    }
}
