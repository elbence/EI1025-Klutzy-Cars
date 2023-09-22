using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    
    private int collided;

    [SerializeField]
    private DriftScore scoreManager;

    void Start() {
        collided = 0;
    }

    void OnTriggerEnter(Collider col) {
        if (collided == 0) {
            collided = 1;
            Debug.Log("Collided");
            scoreManager.updateScore();
        }
    }

}
