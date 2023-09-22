using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       ResetScores(); 
    }

    public void ResetScores() {
        PlayerPrefs.DeleteAll();
    }
}
