using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

	//Ce code permettra de traiter les inputs, de les faire valider via la BDD
	public class Input_authentification : MonoBehaviour {
			public InputField firstName;
			public InputField lastName;
			public Button continue_b;

			// Use this for initialization
			void Start () {
				Set_interactable (false); // make the button uninteractable at the begining
			}
			
			// Update is called once per frame
			void Update () {
				if (firstName.text != "" && lastName.text != "") // if the fields has been filled
					Set_interactable(true);
				else
						Set_interactable(false);
			}
				
			void Set_interactable (bool b)
			{
				continue_b.interactable = b;
			}
		}
