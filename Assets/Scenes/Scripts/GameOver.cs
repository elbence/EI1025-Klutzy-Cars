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

    [SerializeField]
    private AudioSource m_MyAudioSource;


    public CarController player;
    public GameObject menuGameOver;

    private bool notShown;

    // Start is called before the first frame update
    void Start()
    {   
        m_MyAudioSource.Play();
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
            setHighScore(farmScore.getScore());
            m_MyAudioSource.Stop();
        }
    }

    void setHighScore(float scoreAct) {
        // retrieve current highScores, search for firs higher than me
        int i = 0;
        float savedScore = PlayerPrefs.GetFloat("FScore" + i, -1.0f);
        while (savedScore < scoreAct && savedScore != -1.0f) {
            i++;
            savedScore = PlayerPrefs.GetFloat("FScore" + i, -1.0f);
        }
        // save in past score found index, iterate and swap rest
        float pastVal;
        while (i < 5) {
            pastVal = PlayerPrefs.GetFloat("FScore" + i, -1.0f);
            PlayerPrefs.SetFloat("FScore" + i, scoreAct);
            i++;
            scoreAct = pastVal;
        } 
    }
}
