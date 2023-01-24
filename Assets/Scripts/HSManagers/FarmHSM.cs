using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FarmHSM : MonoBehaviour
{   

    [SerializeField]
    private TextMeshProUGUI[] scores;

    // Start is called before the first frame update
    void Start()
    {
        // mirar clavesValor, asignar valores
        int i = 0;
        float scoreAct = PlayerPrefs.GetFloat("FScore" + i, -1.0f);
        while (scoreAct > -1.0f) {
            // asigna valor
            scores[i].text = string.Format("{0:N3}", scoreAct);
            // itera
            i++;
            // recupera nuevo valor
            scoreAct = PlayerPrefs.GetFloat("FScore" + i, -1.0f);
        }
        // rellena resto
        while (i < 5) {
            scores[i].text = "-";
            i++;
        }
    }

    public void RecallStart() {
        Start();
    }
}
