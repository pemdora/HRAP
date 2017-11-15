using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour {

	// Use this for initialization
	void Start () {

        /* QCM qcm = new QCM();
         qcm.LancementQCM();*/

        M_Candidate candidate = new M_Candidate("Tom");
        M_QCM qcm = new M_QCM();
        Debug.Log("tamere");


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
