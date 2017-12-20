using UnityEngine;
using RogoDigital.Lipsync;
using System.IO;
using System.Linq;
using UnityEditor;
using System.Collections;
using UnityEngine.Events;

public class AISpeechManager : MonoBehaviour
{

    public LipSync lipsyncComponent;
    private LipSyncData clip = null;
    public string inFolder = "/Audio/AudioClip";
    public string path = "Assets/Audio/AudioClip/";
    public bool speakTrigger;

    public static AISpeechManager speechManagerinstance;
    //SINGLETON
    void Awake()
    {
        if (speechManagerinstance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        else
        {
            speechManagerinstance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        speakTrigger=false;
    }

    public void LoadandPlayAudio(string id)
    {
        // load clip from value given for P_Interview
        clip = (LipSyncData)AssetDatabase.LoadAssetAtPath(path + id + ".asset", typeof(LipSyncData));
        // P_interview told that AI is going speak soon
        speakTrigger = true;
        // if the candidate wants to begin dialogue (he cannot move anymore)
        if (!CandidateController.candidateControllerInstance.canMove)
        {
            // Play audio
            PlayLipSync();
            speakTrigger = false;
        }
    }

    private void PlayLipSync()
    {
        if (clip != null)
        {
            lipsyncComponent.Play(clip);
        }
        else
        {
            Debug.Log("error");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // if the candidate wants to begin dialogue (he cannot move anymore)
        // we trigger this when he has reached a goal point
        if ((!CandidateController.candidateControllerInstance.canMove)&&speakTrigger)
        {
            PlayLipSync();
            speakTrigger = false;
        }
    }

}
