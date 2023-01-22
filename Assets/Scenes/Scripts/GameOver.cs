using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public CarController player;
    public GameObject menuGameOver;
    // Start is called before the first frame update
    void Start()
    {
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0.0f) {
            menuGameOver.SetActive(true);
		    Cursor.lockState = CursorLockMode.None;
		    Cursor.visible = true;
		    Time.timeScale = 0;
        }
    }
}
