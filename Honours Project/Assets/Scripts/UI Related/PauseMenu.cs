using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour {

	private static bool PausedGame = false;
	public GameObject PauseMenuUI; 
	private GameObject PauseButton;

	void Start(){
		PauseButton = GameObject.Find("PauseButton");
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			if (PausedGame){
				Resume();
			} else if (!PausedGame) {
				Pause(); 
			}
		}
	}

	public void Resume(){
		PauseButton.SetActive(true);
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		PausedGame = false; 
	}

	public void Pause(){
		PauseButton.SetActive(false);
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		PausedGame = true; 
	}
	public void LoadMenu(){
		Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen");
	}

	public void QuitGame(){
		Application.Quit();
	}
}