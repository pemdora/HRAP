using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour
{
    private M_Candidate candidate;
    private P_Interview interview;
    private IHMInterview ihm;

    // Use this for initialization
    void Awake () {
        // We initialise model, view and Presenter with name of candidate and his job
        ihm = GetComponent<IHMInterview>();

        candidate = new M_Candidate("Steve", "Chef de Projet");
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
            ihm.Over();
        }
    }
}
