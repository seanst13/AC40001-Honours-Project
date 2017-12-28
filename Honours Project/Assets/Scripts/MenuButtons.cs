using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

public Canvas diffpanelCanvas;
public Canvas rulesPanelCanvas;

private bool opendiff = false;
private bool openrule = false; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DiffPanelState(){
			if (opendiff == false){
				opendiff = true;
				diffpanelCanvas.enabled = true; 
			}
			else if (opendiff == true){
				opendiff = false;
				diffpanelCanvas.enabled = false; 
			}
	}
		public void rulePanelState(){
			if (openrule == false){
				openrule = true;
				rulesPanelCanvas.enabled = true; 
			}
			else if (openrule == true){
				openrule = false;
				rulesPanelCanvas.enabled = false; 
			}
	}

	public void exitGame(){
		Application.Quit();
	}

}
