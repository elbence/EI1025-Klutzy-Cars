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
        scoreText.text = string.Format("{0:N2}", driftScore.getScore()) + " points";
    } 

}
