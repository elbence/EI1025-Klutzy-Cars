using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmScore : MonoBehaviour
{

    private float timer;

    private CarController carController;

    private CarController player;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            player =  GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        }
        timer = 0.0f;
    }

    void Update() {
        if (player == null) {
            if (GameObject.FindGameObjectWithTag("Player") != null) {
                player =  GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
            }        
        }
        if (player.currentHealth > 0.0f) {  // if alive
            timer += Time.deltaTime;        // add score
        }
    }

    public float getScore() {
        return timer;
    }
}
