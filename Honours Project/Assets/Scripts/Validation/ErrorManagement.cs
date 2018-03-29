using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ErrorManagement : MonoBehaviour {
	public GameObject ErrorWindow;
	public Text message; 
	public static ErrorManagement instance; 

	void Awake() {
		instance = this; 
	}

	public void ShowError(string ErrorMessage){
		message.text = ErrorMessage;
		ErrorWindow.SetActive(true);

	}

	public void HideError(){
		ErrorWindow.SetActive(false);
	}
}