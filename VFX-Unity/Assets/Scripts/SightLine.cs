/***
 * Author: Jacob Sharp
 * Created: 11/16/2022
 * Modified: 
 * Description: 
 ***/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SightLine : MonoBehaviour
{
    public Transform eyePoint;
    public string targetTag = "Player";
    public float fieldOfView = 45f;

    public bool targetVisible { get; set; } = false;
    public Vector3 lastKnownSighting { get; set; } = Vector3.zero;
    private SphereCollider sightCollider;

    void Awake()
    {
        sightCollider = GetComponent<SphereCollider>();
        lastKnownSighting = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            UpdateSight(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetVisible = false;
        }
    }

    private void UpdateSight(Transform target)
    {
        targetVisible = targetInFOV(target) && targetInClearLOS(target);
        if (targetVisible)
        {
            lastKnownSighting = target.position;
            
        }
    }

    private bool targetInFOV(Transform target)
    {
        Vector3 targetDir = target.position - eyePoint.position;
        float angle = Vector3.Angle(eyePoint.forward, targetDir);

        if (angle <= fieldOfView) return true;
        return false;
    }

    private bool targetInClearLOS(Transform target)
    {
        RaycastHit hit;
        Vector3 targetDir = (target.position - eyePoint.position).normalized;

        if (Physics.Raycast(eyePoint.position, targetDir + Vector3.up / 4f, out hit, sightCollider.radius))
        {
            Debug.Log(targetDir + "   | hit: " + hit.collider.name + "   | target: " + target.name); // PROBLEM IS TARGET, ADDING UP FIXES IT

            if (hit.transform.CompareTag(targetTag))
            {
                return true;
            }
        }

        return false;
    }
}
