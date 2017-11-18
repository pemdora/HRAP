using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour {
    UIButton button_a, button_b, button_c, button_d;
    UILabel question_answers_to_display;
    UILabel cname, clastname;

    // Use this for initialization
    void Start () {
        //init UI elements
        button_a = GameObject.Find("ButtonA").GetComponent<UIButton>();
        button_b = GameObject.Find("ButtonB").GetComponent<UIButton>();
        button_c = GameObject.Find("ButtonC").GetComponent<UIButton>();
        button_d = GameObject.Find("ButtonD").GetComponent<UIButton>();
        question_answers_to_display = GameObject.Find("Text_q_a").GetComponent<UILabel>();
        cname = GameObject.Find("cname").GetComponent<UILabel>();
        clastname = GameObject.Find("clastname").GetComponent<UILabel>();
        //Init Candidate and QCM
        //M_Candidate candidate = new M_Candidate("Tom");
       // M_Quizz qcm = new M_Quizz();
        //Debug.Log(candidate.Name);
        //clastname.text = Input_authentification.lastName.label.text;//test
        //cname.text = Input_authentification.firstName.label.text;//test

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
