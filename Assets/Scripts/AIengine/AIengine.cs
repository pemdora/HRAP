using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour {

	// Use this for initialization
	void Start () {

        QCM qcm = new QCM();
        qcm.LancementQCM();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
