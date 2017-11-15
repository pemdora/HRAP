	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine; 
	using UnityEngine.UI;

		//Ce code permettra de traiter les inputs, de les faire valider via la BDD
		public class Input_authentification : MonoBehaviour {
		public static UIInput firstName;
		public static UIInput lastName;
		//public UIPopupList listPoste;
	    UILabel poplist_label;
		UIButton bouton_continuer;
    // Use this for initialization
    void Start () {
			bouton_continuer = GameObject.Find("Bouton_continuer").GetComponent<UIButton>();
			firstName = GameObject.Find ("Input_name").GetComponent<UIInput> ();
			lastName = GameObject.Find ("Input_lastname").GetComponent<UIInput> ();
		    poplist_label = GameObject.Find("pop_list_label").GetComponent<UILabel>();

			Set_interactable (false); // make the button uninteractable at the begining
		}
				
				// Update is called once per frame
				void Update () {
		if (firstName.value != "" && lastName.value != "" && firstName.value.Length >= 2 && lastName.value.Length >=2 && poplist_label.text != "Choix du poste") // if the fields has been filled
						Set_interactable(true);
					else
							Set_interactable(false);
				}
					
				void Set_interactable (bool b)
				{
		bouton_continuer.isEnabled = b;
				}
			}
