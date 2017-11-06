﻿using UnityEngine;
using UnityEngine.UI;

public class On_scroll_vertical_end : MonoBehaviour {

	Scrollbar vertical_scrollbar;
	Button button_continue;

	// Use this for initialization
	void Start(){
		
		button_continue = GameObject.Find("button_continue").GetComponent<Button>();
		vertical_scrollbar = GameObject.Find ("Scrollbar Horizontal").GetComponent<Scrollbar>();
		button_continue.interactable = false;
		//Set_Interactable_Button (false); // rends indisponible le bouton
	}

	// Update is called once per frame
	void Update () {
		if (vertical_scrollbar.value == 0) // tant que le scroll n'est pas en bas
		{
			button_continue.interactable = true;
			//Set_Interactable_Button (true);// rendre le bouton clickable 
		}
	}

	void Set_Interactable_Button(bool b)
	{
		 button_continue.interactable = b;
	}
}
