using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
	public GameObject menuDePausa;
	public GameObject menuDeSettings;

    private bool menuOn;
	private bool escPulsado;

	void Start() {
		escPulsado = false;
		menuDesactivar();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && !menuDeSettings.activeSelf) // check if settings menu is active !!
		{
			escPulsado = true;
			menuOn = !menuOn;
		}

		if (escPulsado) {
			escPulsado = false;	
			if (menuOn == true) {
				menuActivar();
			} else {
				menuDesactivar();
			}
		}

	}

	public void Continuar()
	{
		menuDesactivar();
		menuOn = false;
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	private void menuActivar() {
		menuDePausa.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0;
	}

	private void menuDesactivar()
	{
		menuDePausa.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1;
	}
}