using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tutorial used: https://www.youtube.com/watch?v=RwGIyRy-Lss
public class HandPhysics : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotOffset;
    private Transform followTarget;
    private Rigidbody body;

    private void Start()
    {
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }

    private void Update()
    {
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        Vector3 posWithOffset = followTarget.position + posOffset;

        float distance = Vector3.Distance(posWithOffset, transform.position);
        body.velocity = (posWithOffset - transform.position).normalized * (followSpeed * distance);


        Quaternion rotWithOffset = followTarget.rotation * Quaternion.Euler(rotOffset);

        Quaternion q = rotWithOffset * Quaternion.Inverse(transform.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        if(angle > 180f) { angle -= 360f; }
        Vector3 angularVel = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        if (angularVel.x != float.NaN && angularVel.y != float.NaN && angularVel.z != float.NaN)
        {
            body.angularVelocity = angularVel;
        }
    }
}
