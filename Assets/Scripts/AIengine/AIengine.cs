using HRAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIengine : MonoBehaviour {
    P_Interview interview; 

    // Use this for initialization
    void Start () {

        // 1. On instancie P_Interview avec le nom du candidat et son poste
        interview = new P_Interview("steve", "chef de projet");
        interview.Launch();

    }
	

    // TEST
    // THIS HAS TO BE MOVED TO VIEW CLASS
    public static void AffichageQuestion(string questionToDisplay) // ne dois pas être statique dans la fonction finale
    {
        Debug.Log(questionToDisplay);
    }
    public static void AffichageRéponses(List<string> questionsToDisplay) // ne dois pas être statique dans la fonction finale
    {
        foreach (string questionToDisplay in questionsToDisplay)
        {
            Debug.Log(questionToDisplay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Choix enregistré");
            interview.SetChosenAnswer(1);
        }

    }
}
