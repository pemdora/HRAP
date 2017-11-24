using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ihm_script : MonoBehaviour {
    UIButton button_a, button_b, button_c, button_d;
    UILabel question_answers_to_display;
    UILabel cname, clastname;
    string firstname = "";
    string lastname= "";
    // Use this for initialization
    void Start () {
        button_a = GameObject.Find("ButtonA").GetComponent<UIButton>();
        button_b = GameObject.Find("ButtonB").GetComponent<UIButton>();
        button_c = GameObject.Find("ButtonC").GetComponent<UIButton>();
        button_d = GameObject.Find("ButtonD").GetComponent<UIButton>();
        question_answers_to_display = GameObject.Find("Text_q_a").GetComponent<UILabel>();
        cname = GameObject.Find("cname").GetComponent<UILabel>();
        clastname = GameObject.Find("clastname").GetComponent<UILabel>();
        //firstname = Input_authentification.firstName.value;   those variable will collect the input from the last scene
        //lastname = Input_authentification.lastName.value;

    }
	
	// Update is called once per frame
	void Update() {
        //Collection of variables from the authentication scene
        if (firstname != "" && lastname != "")
        {
            cname.text = Input_authentification.firstName.value;
            clastname.text = Input_authentification.lastName.value;
        }


	}
}
