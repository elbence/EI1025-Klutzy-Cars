using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Settings
    public float currentHealth;
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;
    public TrailRenderer[] trails;

    // Variables
    private HealthBar healthBar;
    private Vector3 MoveForce;
    private float AxisVertical;
    private float AxisHorizontal;
    private float TrailAngle;
    private float maxHealth = 100f;
    private Vector3 TransformAdjusted;
    private bool touchingFloor;

    Rigidbody rb;
    public float speed = 50f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        if (GameObject.FindGameObjectWithTag("HealthBar") != null) {
            healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
            currentHealth = maxHealth;
            healthBar.setMaxHealth(maxHealth);
        }
        TransformAdjusted = new Vector3( transform.position.x, transform.position.y + 1, transform.position.z );
    }

    // Update is called once per frame
    void FixedUpdate() {

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
            if (hit.distance <= 1.20) {
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
            if (AxisVertical - 0.15f >= 0) {
                AxisVertical -= 0.15f;
            } else if (AxisVertical != 0) {
                AxisVertical = 0f;
            }
        }

        // Debug.Log(AxisHorizontal);
        // Debug.Log(AxisVertical);


        // Moving (Calc force + move transform)
        MoveForce += transform.forward * MoveSpeed * AxisVertical * Time.deltaTime;
        //transform.position += MoveForce * Time.deltaTime;
        
        Vector3 tmp = transform.forward * AxisVertical * speed;
        rb.velocity =  new Vector3(tmp.x, rb.velocity.y, tmp.z);


        // Steering
        float steerInput = AxisHorizontal;
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);


        // Drag and max speed limit
        MoveForce *= Drag;
        // If going forward
            MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);
        // If going reverse (less speed)
            // Code here


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
        AxisHorizontal = Input.GetAxis("Horizontal");
        AxisVertical = Input.GetAxis("Vertical");
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Enemy")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Hitted by police!");
            currentHealth -= 10f;
            healthBar.setHealth(currentHealth);
        }
    }
}
