using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftScore : MonoBehaviour
{
    // This script is supposed to go on the father of the checkpoints
    [SerializeField]
    private GameOverDrift gameOverScreen;

    private List<GameObject> checkpoints;
    private int activeCheckpoint;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        // fill list of checkpoints
        checkpoints = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++) {
            checkpoints.Add(transform.GetChild(i).gameObject);
            Debug.Log(transform.GetChild(i).name);
        }
        // deactivate all except first
        foreach(GameObject check in checkpoints) {
            check.SetActive(false);
        }
        checkpoints[0].SetActive(true);
        activeCheckpoint = 0;
    }

    public void updateScore() {
        checkpoints[activeCheckpoint].SetActive(false);
        activeCheckpoint++;
        Debug.Log(activeCheckpoint);
        if (activeCheckpoint < checkpoints.Count) {
            checkpoints[activeCheckpoint].SetActive(true);
        } else {
            Debug.Log("********END GAME*********");
            Debug.Log("Score:");
            Debug.Log(timer);
            gameOverScreen.ShowEndScreen();
            // CODE HERE
        }
        
    }


    void Update() {
        if (activeCheckpoint < checkpoints.Count) { 
            timer += Time.deltaTime;
        }
    }


    public float getScore() {
        return timer;
    } 

}
