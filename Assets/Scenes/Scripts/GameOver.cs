using System.Collections;
using System.Collections.Generic;
using UnityEngine;    
using TMPro;


public class GameOver : MonoBehaviour
{

    [SerializeField]
    private FarmScore farmScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;


    public CarController player;
    public GameObject menuGameOver;

    private bool notShown;

    // Start is called before the first frame update
    void Start()
    {   
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            player =  GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        }
        notShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            if (GameObject.FindGameObjectWithTag("Player") != null) {
                player =  GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
            }        
        } else if (player.currentHealth <= 0.0f && !notShown) {
            notShown = true;
            menuGameOver.SetActive(true);
		    Cursor.lockState = CursorLockMode.None;
		    Cursor.visible = true;
		    Time.timeScale = 0;
            Debug.Log(farmScore.getScore());
            scoreText.text = string.Format("{0:N2}", farmScore.getScore()) + " points";
        }
    }
}
