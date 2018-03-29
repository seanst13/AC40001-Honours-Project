using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

public Canvas diffpanelCanvas;
public Canvas rulesPanelCanvas;

private bool opendiff = false;
private bool openrule = false; 

	public void DiffPanelState(){
			if (!opendiff){
				opendiff = true;
				diffpanelCanvas.enabled = true; 
			}
			else if (opendiff){
				opendiff = false;
				diffpanelCanvas.enabled = false; 
			}
	}
		public void rulePanelState(){
			if (!openrule){
				openrule = true;
				rulesPanelCanvas.enabled = true; 
			}
			else if (openrule){
				openrule = false;
				rulesPanelCanvas.enabled = false; 
			}
	}

	public void exitGame(){
		Application.Quit();
	}


	public void loadScene(){
		SceneManager.LoadScene("GameScreen");
	}
}
