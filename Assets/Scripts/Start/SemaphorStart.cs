using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaphorStart : MonoBehaviour
{
    private List<GameObject> red3;
    private List<GameObject> red2;
    private List<GameObject> red1;
    private List<GameObject> yellow;

    // Start is called before the first frame update
    void Start()
    {
        red3 = new List<GameObject>();
        red2 = new List<GameObject>();
        red1 = new List<GameObject>();
        yellow = new List<GameObject>();

        Debug.Log("Start - Semaphor");

        red3.Add(transform.GetChild(0).gameObject);    
        red3.Add(transform.GetChild(1).gameObject);

        red2.Add(transform.GetChild(2).gameObject);    
        red2.Add(transform.GetChild(3).gameObject);    

        red1.Add(transform.GetChild(4).gameObject);    
        red1.Add(transform.GetChild(5).gameObject);    

        yellow.Add(transform.GetChild(6).gameObject);    
        yellow.Add(transform.GetChild(7).gameObject);

        for(int i = 0; i < 2; i++) {
            Debug.Log(red3[i]);
            Debug.Log(red2[i]);
            Debug.Log(red1[i]);
            Debug.Log(yellow[i]);
        }

        StartCoroutine(PlayStartAnimation());

    }


    void TurnOff() {
        foreach(GameObject obj in red3) {
            obj.SetActive(false);
        }
        foreach(GameObject obj in red2) {
            obj.SetActive(false);
        }
        foreach(GameObject obj in red1) {
            obj.SetActive(false);
        }
        foreach(GameObject obj in yellow) {
            obj.SetActive(false);
        }
    }

    void TurnOn() {
        foreach(GameObject obj in red3) {
            obj.SetActive(true);
        }
        foreach(GameObject obj in red2) {
            obj.SetActive(true);
        }
        foreach(GameObject obj in red1) {
            obj.SetActive(true);
        }
        foreach(GameObject obj in yellow) {
            obj.SetActive(true);
        }
    }

    IEnumerator PlayStartAnimation() {
        
        // turn all on
        TurnOn();
        // wait 2 s
        yield return new WaitForSeconds(2);
        
        // turn all off
        TurnOff();
        // wait 2 s
        yield return new WaitForSeconds(2);
        
        // turn 3 on
        foreach(GameObject obj in red3) {
            obj.SetActive(true);
        }
        // wait .5 s
        yield return new WaitForSeconds(0.5f);
        
        // turn 3 off 2 on
        foreach(GameObject obj in red3) {
            obj.SetActive(false);
        }
        foreach(GameObject obj in red2) {
            obj.SetActive(true);
        }
        // wait .5 s
        yield return new WaitForSeconds(0.5f);        
        
        // turn 2 off 1 on
        foreach(GameObject obj in red2) {
            obj.SetActive(false);
        }
        foreach(GameObject obj in red1) {
            obj.SetActive(true);
        }
        // wait .5 s
        yield return new WaitForSeconds(0.5f);        
        
        // turn 1 off yellow on
        foreach(GameObject obj in red1) {
            obj.SetActive(false);
        }
        foreach(GameObject obj in yellow) {
            obj.SetActive(true);
        }
        // wait .5 s
        yield return new WaitForSeconds(0.5f);        
        
        // turn yellow off, START RACE
        foreach(GameObject obj in yellow) {
            obj.SetActive(false);
        }
    }

}
