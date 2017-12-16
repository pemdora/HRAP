using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour
{
    private M_Candidate candidate;
    private P_Interview interview;
    private IHMInterview ihm;
    private CameraManager cameraManager;
    // ChatManager chatm;

    // TODO has to get arguments from previous view
    // private string candidateName;
    // private string targetJob; 

    // Use this for initialization
    void Start () {
        // 1. We initialise model, view and Presenter with name of candidate and his job
        ihm = GetComponent<IHMInterview>();
       // chatm = GetComponent<ChatManager>();
        candidate = new M_Candidate("steve", "chef de projet"); // TODO has to get arguments from previous view
        interview = new P_Interview(candidate, ihm);

        ihm.SetPresenter(interview);
    }

    // Update is called once per frame
    void Update()
    {
        if (!interview.IsOver)
        {
            interview.Launch();
        }
        else
        {
            // TODO : afficher le résultat dans la vue
        }
    }
}
