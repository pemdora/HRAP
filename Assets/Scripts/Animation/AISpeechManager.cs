using UnityEngine;
using RogoDigital.Lipsync;

public class AISpeechManager : MonoBehaviour
{

    public LipSync lipsyncComponent;
    private LipSyncData clip = null;
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

    public float GetLengthAudioClip()
    {
        if (this.clip != null)
        {
            return this.clip.length;
        }
        else
        {
            return 0f;
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
        clip = Resources.Load(id) as LipSyncData; // Load resource using Resource folder in Assets folder
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
