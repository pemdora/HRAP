using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogoDigital.Lipsync;

public class SpeechManager : MonoBehaviour {

    public LipSync lipsyncComponent;
    public LipSyncData clip1;
    public LipSyncData clip2;
    int i = 0;

    // Use this for initialization
    void Start ()
    {
        lipsyncComponent.Play(clip1);
    }
	
	// Update is called once per frame
	void Update () {
    }
}
