	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine; 
	using UnityEngine.UI;

//Ce code permettra de traiter les inputs
public class IHMAuthentification : MonoBehaviour
{
    public static UIInput firstName;
    public static UIInput lastName;
    public static UILabel poplist_label;
    UIButton bouton_continuer, bouton_retour;
    bool isClicked; // notify when the button is clicked in order to disable all buttons

    // Use this for initialization
    void Start()
    {
        isClicked = false;
        bouton_retour = GameObject.Find("Bouton_retour").GetComponent<UIButton>();
        bouton_continuer = GameObject.Find("Bouton_continuer").GetComponent<UIButton>();
        firstName = GameObject.Find("Input_name").GetComponent<UIInput>();
        lastName = GameObject.Find("Input_lastname").GetComponent<UIInput>();
        poplist_label = GameObject.Find("pop_list_label").GetComponent<UILabel>();

        //bouton_continuer.gameObject.GetComponent<TransitionalObject>().enabled = false; // play anim
        Set_interactable(false); // make the button uninteractable at the begining
        bouton_retour.isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isClicked ==false && firstName.value.Length >= 2 && lastName.value.Length >= 2 && poplist_label.text != "Choix du poste") // if the fields has been filled
        {
            Set_interactable(true);
            bouton_continuer.gameObject.GetComponent<TransitionalObject>().enabled = true; // play anim

        }
            
        else
        {
            Set_interactable(false);
            bouton_continuer.gameObject.GetComponent<TransitionalObject>().enabled = false;

        }


        if (firstName.isSelected && Input.GetKeyDown(KeyCode.Tab))
        {
            firstName.isSelected = false;
            lastName.isSelected = true;
        }

        else if (lastName.isSelected && Input.GetKeyDown(KeyCode.Tab))
        {
            lastName.isSelected = false;
            firstName.isSelected = true;
        }
    }

    public void OnClick()
    {
        bouton_continuer.gameObject.GetComponent<TransitionalObject>().enabled = false; // play animation
        bouton_retour.isEnabled = false;
        isClicked = true;
    }
    void Set_interactable(bool b)
    {
        bouton_continuer.isEnabled = b;
    }
}
