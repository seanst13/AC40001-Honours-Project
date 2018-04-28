using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableObjects : MonoBehaviour {

	private Transform draggingObject;
	private Vector2 originalObjectPosition; 
	private bool currentlyDragging = false; 
	private string matchingTag = "Draggable";
	private Image draggingImage; 

	List<RaycastResult> foundElements = new List<RaycastResult>();
	void Update () {
		if (Input.GetMouseButton(0)){
			draggingObject = returnTransformUnderMouse(); 

			if (draggingObject !=null){
				currentlyDragging = true;
				draggingObject.SetAsLastSibling();

                originalObjectPosition = draggingObject.position;
                draggingImage = draggingObject.GetComponent<Image>();
                draggingImage.raycastTarget = false;
		}

		if (currentlyDragging){
			draggingObject.position = Input.mousePosition;
		}
	}
	}

	private GameObject returnGameObjectUnderMouse(){
		var pointer = new PointerEventData(EventSystem.current);

		pointer.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointer, foundElements);

		if (foundElements.Count == 0){
			return null;
		} else {
			return foundElements[0].gameObject; 
		}
	}

	private Transform returnTransformUnderMouse(){

		GameObject clickedObject = returnGameObjectUnderMouse(); 

		if (clickedObject != null && clickedObject.tag == matchingTag){
			return clickedObject.transform;
		} else {
			return null; 
		}

	}
}
