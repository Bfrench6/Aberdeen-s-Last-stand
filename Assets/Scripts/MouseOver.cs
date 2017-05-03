//Ben French, Chuan Yui, Pranav Bhardwaj

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject stroke;

	public void OnPointerEnter(PointerEventData eventData) {
		stroke.SetActive (true);
	}

	public void OnPointerExit(PointerEventData eventData) {
		stroke.SetActive (false);
	}
		
}
