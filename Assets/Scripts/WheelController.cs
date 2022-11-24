using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private Rigidbody rb;

    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelBackLeft;
    public bool wheelBackRight;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;


    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springForce;
    private float damperForce;
    private float springVelocity;
    private Vector3 suspensionForce;
    private float wheelAngle;
    private Vector3 wheelVelocityLS; // Local Space
    private float forceInX;
    private float forceInY;

    [Header("Wheel")]
    public float wheelRadius;
    public float steerAngle;
    public float steerTime;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);
    } 

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;

            springLength = hit.distance - wheelRadius;

            springLength = Mathf.Clamp(springLength, minLength, maxLength);

            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;

            springForce = springStiffness * (restLength - springLength);

            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            forceInX = Input.GetAxis("Vertical") * 0.5f * springForce;
            forceInY = wheelVelocityLS.x * springForce;

            rb.AddForceAtPosition(suspensionForce + (forceInX * transform.forward) + (forceInY * -transform.right), hit.point);

        }
    }
}
