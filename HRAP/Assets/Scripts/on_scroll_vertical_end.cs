using UnityEngine;
using UnityEngine.UI;

public class on_scroll_vertical_end : MonoBehaviour {
	public  Scrollbar vertical_scrollbar;
	public  Button button_continue;

	// Use this for initialization
	void Start () {

		Set_Interactable_Button (false); // rends indisponible le bouton 
	}
	
	// Update is called once per frame
	void Update () {
		if (vertical_scrollbar.value == 0) // tant que le scroll n'est pas en bas
		{
			Set_Interactable_Button (true);// rendre le bouton clicable 
		}
	}

	void Set_Interactable_Button(bool b)
	{
		button_continue.interactable = b;
	}
}
