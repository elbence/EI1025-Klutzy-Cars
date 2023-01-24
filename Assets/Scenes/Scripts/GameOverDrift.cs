using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverDrift : MonoBehaviour
{
    [SerializeField]
    private DriftScore driftScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public GameObject menuGameOver;

    public void ShowEndScreen() {
        menuGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        Debug.Log(driftScore.getScore());
        scoreText.text = string.Format("{0:N3}", driftScore.getScore()) + " points";
        setHighScore(driftScore.getScore());
    }

    void setHighScore(float scoreAct) {
        // retrieve current highScores, search for firs higher than me
        int i = 0;
        float savedScore = PlayerPrefs.GetFloat("DScore" + i, -1.0f);
        while (savedScore < scoreAct && savedScore != -1.0f) {
            i++;
            savedScore = PlayerPrefs.GetFloat("DScore" + i, -1.0f);
        }
        // save in past score found index, iterate and swap rest
        float pastVal;
        while (i < 5) {
            pastVal = PlayerPrefs.GetFloat("DScore" + i, -1.0f);
            PlayerPrefs.SetFloat("DScore" + i, scoreAct);
            i++;
            scoreAct = pastVal;
        } 
    }

}
