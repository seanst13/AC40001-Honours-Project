using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour {

	public static bool PausedGame = false;
	public GameObject PauseMenuUI; 

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
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		PausedGame = false; 
	}

	void Pause(){
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		PausedGame = true; 
	}
	public void LoadMenu(){
        SceneManager.LoadScene("Title Screen");
	}

	public void QuitGame(){
		Application.Quit();
	}
}