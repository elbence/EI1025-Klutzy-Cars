using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftLightsManager : MonoBehaviour
{
    private GameObject lightCubesFather;
    private GameObject spotLights;

    private List<GameObject> allFocusLights;
    
    private List<GameObject> allLightCubes;
    private List<GameObject> allSpotLightsFathers;
    
    [SerializeField]
    private Material onMaterial;

    [SerializeField]
    private Material offMaterial;

    void Start () {
        allFocusLights = new List<GameObject>();
        allLightCubes = new List<GameObject>();
        allSpotLightsFathers = new List<GameObject>();

        Debug.Log("Dads:");
        for (int i = 0; i < transform.childCount; i++) {
            allFocusLights.Add(transform.GetChild(i).gameObject);
            Debug.Log(transform.GetChild(i).name);
        }
        
        Debug.Log("Childs:");
        GameObject actCubes;
        foreach(GameObject focusLight in allFocusLights) {
            actCubes = focusLight.transform.GetChild(1).gameObject;
            for (int i = 0; i < actCubes.transform.childCount; i++) {
                allLightCubes.Add(actCubes.transform.GetChild(i).gameObject);
                Debug.Log(actCubes.transform.GetChild(i).name);
            }
            allSpotLightsFathers.Add(focusLight.transform.GetChild(2).gameObject);
            Debug.Log(focusLight.transform.GetChild(2).name);

        }

        // myRend = gameObject.GetComponent<MeshRenderer> ();

    }


    //called once we know its night.
    public void TurnOn()
    {   
        MeshRenderer myRend;

        // for each children - activate
        foreach(GameObject actCube in allLightCubes) {
            // Change texture
            myRend = actCube.GetComponent<MeshRenderer> ();
            Material[] materials = myRend.materials;
            materials[0] = onMaterial;
            myRend.materials = materials;
        }
        // activate lights 
        foreach(GameObject actSpotLightFather in allSpotLightsFathers) {
            actSpotLightFather.SetActive(true);
        }
    }
    // Material[] materials = myRend.materials;
    // materials[2] = onMaterial;
    // myRend.materials = materials;
    // Debug.Log("ON - name: " + myRend.materials[2].name);


    //called once we know its day.
    public void TurnOff()
    {   
        MeshRenderer myRend;

        // for each children - activate
        foreach(GameObject actCube in allLightCubes) {
            // Change texture
            myRend = actCube.GetComponent<MeshRenderer> ();
            Material[] materials = myRend.materials;
            materials[0] = offMaterial;
            myRend.materials = materials;
        }
        // activate lights 
        foreach(GameObject actSpotLightFather in allSpotLightsFathers) {
            actSpotLightFather.SetActive(false);
        }
    }
    // Material[] materials = myRend.materials;
    // materials[2] = offMaterial;
    // myRend.materials = materials;
    // Debug.Log("OFF - name: " + myRend.materials[2].name);
}
