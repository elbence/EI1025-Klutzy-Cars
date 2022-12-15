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

    void Start() {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update() {

        // Getting the inputs
        GetInputs();

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
