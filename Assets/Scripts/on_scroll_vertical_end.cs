using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class on_scroll_vertical_end : MonoBehaviour {

	Scrollbar vertical_scrollbar;
	Button button_continue;
	UIToggle cb;
	// Use this for initialization
	void Start(){
		try{
			button_continue = GameObject.Find("button_continue").GetComponent<Button>();
			vertical_scrollbar = GameObject.Find ("Scrollbar Vertical").GetComponent<Scrollbar>();
			cb = GameObject.Find ("Checkbox").GetComponent<UIToggle> (); 
		}catch(UnityException e){
			Debug.LogException (e, this);
		
		}

		Set_Interactable_Button(false);
		vertical_scrollbar.enabled = true;
		//Set_Interactable_Button (false); // rends indisponible le bouton
	}

	// Update is called once per frame
	void Update () {
		if (vertical_scrollbar.value == 0 && cb.isChecked) { // tant que le scroll n'est pas en bas
			Set_Interactable_Button (true);// rendre le bouton clickable 
		} else
			Set_Interactable_Button (false);
	}

	void Set_Interactable_Button(bool b)
	{
		 button_continue.interactable = b;
	}
}
