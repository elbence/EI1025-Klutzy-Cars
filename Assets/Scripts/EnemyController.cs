using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
       // Settings
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;
    public TrailRenderer[] trails;
    public Transform target;

    // Variables
    private Vector3 MoveForce;
    private float AxisVertical;
    private float AxisHorizontal;
    private float TrailAngle;
    private float reaction;

    private Vector3 TransformAdjusted;
    private bool touchingFloor;


    // Update is called once per frame
    void FixedUpdate() {

        if (target == null) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 0;

        RaycastHit hit;
        TransformAdjusted.x = transform.position.x;
        TransformAdjusted.y = transform.position.y + 1;
        TransformAdjusted.z = transform.position.z;
    
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(TransformAdjusted, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(TransformAdjusted, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            // Debug.Log("Did Hit");
            if (hit.distance <= 1.08) {
                touchingFloor = true;
            } else {
                touchingFloor = false;
            }
        }
        else
        {
            Debug.DrawRay(TransformAdjusted, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            // Debug.Log("Did not Hit");
            touchingFloor = false;
        }
        
        // Getting the inputs
        if (touchingFloor) {
            GetInputs();
        }
        // Si no toca suelo || volcado
        else {
            if (AxisVertical - 0.08f >= 0) {
                AxisVertical -= 0.08f;
            } else if (AxisVertical != 0) {
                AxisVertical = 0f;
            }
        }

        // Moving
        MoveForce += transform.forward * MoveSpeed * AxisVertical * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;

        // Steering
        float steerInput = AxisHorizontal;
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
    
        // Trails render
        TrailAngle = Vector3.Dot(transform.forward, MoveForce.normalized);
        // Debug.Log(TrailAngle);
        foreach(TrailRenderer trail in trails) {
            if (TrailAngle >= 0f && TrailAngle <= 0.93f) {
                trail.emitting = true;
            } else {
                trail.emitting = false;
            }
        }
    }

    void GetInputs() {
        float targetPlaneAngle = vector3AngleOnPlane(target.position, transform.position, -transform.up, transform.forward);
        Vector3 newRotation = new Vector3(0, targetPlaneAngle, 0);
        transform.Rotate(newRotation, Space.Self);

        Vector3 aux = target.position - transform.position;
        reaction = Vector3.Dot(transform.forward, aux.normalized);

        AxisVertical = reaction;
    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

        return projectedVectorAngle;
    } 
}
