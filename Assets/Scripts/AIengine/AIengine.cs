using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour
{
    private M_Candidate candidate;
    private P_Interview interview;
    private IHMInterview ihm;
    public static string datapath;
    private bool end;


    public static AIengine aiEngine;
    public List<V_Competence> result;
    // Use this for initialization
    void Awake()
    {
        if (aiEngine != null)
        {
            Debug.LogError("More than one Candidate Controller in scene");
            return;
        }
        else
        {
            datapath = Application.dataPath;
            // We initialise model, view and Presenter with name of candidate and his job
            ihm = GetComponent<IHMInterview>();

            candidate = new M_Candidate(ihm.GetName(), ihm.GetPosition());
            interview = new P_Interview(candidate, ihm);

            ihm.SetPresenter(interview);
            aiEngine = this;
            end = false;
        }
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
            if (end != true)
            {
                ihm.Over();

                result = interview.GetResult();

                // Save candidate in db
                M_DataManager.Instance.AddCandidate(candidate);
                end = true;
            }
        }
    }

    // Used for debugging
    public static void Display(string msg)
    {
        Debug.Log(msg);
    }
}
