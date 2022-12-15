using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaVolumen : MonoBehaviour
{

	public Slider slider;
	public float sliderValue;
	public Image imagenMute;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
		AudioListener.volume = slider.value;
		
    }
	
	public void ChangeSlider(float valor)
	{
		sliderValue = valor;
		PlayerPrefs.SetFloat("volumenAudio", sliderValue);
		AudioListener.volume = slider.value;
		
	}

    // Update is called once per frame
   
}
