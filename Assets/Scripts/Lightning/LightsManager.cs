using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{   

    private GameObject streetLightsFather;
    private GameObject otherSpotLightsFather;
    
    private List<GameObject> allStreetLights;
    
    [SerializeField]
    private Material onMaterial;

    [SerializeField]
    private Material offMaterial;

    void Start () {
        allStreetLights = new List<GameObject>();

        Debug.Log("Dads:");
        streetLightsFather = transform.GetChild(0).gameObject;
        otherSpotLightsFather = transform.GetChild(1).gameObject;
        Debug.Log(streetLightsFather.name);
        Debug.Log(otherSpotLightsFather.name);
        
        Debug.Log("Childs:");
        for (int i = 0; i < streetLightsFather.transform.childCount; i++) {
            allStreetLights.Add(streetLightsFather.transform.GetChild(i).gameObject);
            Debug.Log(streetLightsFather.transform.GetChild(i).name);
        }

        // myRend = gameObject.GetComponent<MeshRenderer> ();

    }


    //called once we know its night.
    public void TurnOn()
    {   
        MeshRenderer myRend;

        // for each children - activate
        foreach(GameObject myObjct in allStreetLights) {
            // Change texture
            myRend = myObjct.GetComponent<MeshRenderer> ();
            Material[] materials = myRend.materials;
            materials[2] = onMaterial;
            myRend.materials = materials;
            // Activate child
            myObjct.transform.GetChild(0).gameObject.SetActive(true);
            // Debug.Log("ON - name: " + myRend.materials[2].name);
        }
        // activate rest of lights 
        otherSpotLightsFather.SetActive(true);
    }
    // Material[] materials = myRend.materials;
    // materials[2] = onMaterial;
    // myRend.materials = materials;
    // Debug.Log("ON - name: " + myRend.materials[2].name);


    //called once we know its day.
    public void TurnOff()
    {   
        MeshRenderer myRend;

        // for each children - deactivate
        foreach(GameObject myObjct in allStreetLights) {
            // Change texture
            myRend = myObjct.GetComponent<MeshRenderer> ();
            Material[] materials = myRend.materials;
            materials[2] = offMaterial;
            myRend.materials = materials;
            // Deactivate child
            myObjct.transform.GetChild(0).gameObject.SetActive(false);
            // Debug.Log("ON - name: " + myRend.materials[2].name);
        }
        // deactivate other lights
        otherSpotLightsFather.SetActive(false);
    }
    // Material[] materials = myRend.materials;
    // materials[2] = offMaterial;
    // myRend.materials = materials;
    // Debug.Log("OFF - name: " + myRend.materials[2].name);
}
