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
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + inFolder);
        FileInfo[] info = dir.GetFiles("*.asset");
        string[] fullNames = info.Select(f => f.FullName).ToArray();
        foreach (string audioClipPath in fullNames)
        {
            string name = Path.GetFileName(audioClipPath);
            clip = (LipSyncData)AssetDatabase.LoadAssetAtPath(path + name, typeof(LipSyncData));
        }
        /*
        UnityEvent myEvent;
        if (myEvent == null)
            myEvent = lipsyncComponent.onFinishedPlaying;
        myEvent.AddListener(PlayAutoSync); */
    }

    public void PlayAudio(string id)
    {
        clip = (LipSyncData)AssetDatabase.LoadAssetAtPath(path + id + ".asset", typeof(LipSyncData));
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

    }

}
