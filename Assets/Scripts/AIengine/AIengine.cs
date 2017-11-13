using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //QCM qcm = new QCM();
        //qcm.LancementQCM();
        Question quest = new Question(1,"Est ce que vous êtes motivé ?",2);
        Debug.Log(quest.String);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
