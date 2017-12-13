using UnityEngine;
using RogoDigital.Lipsync;
using System.IO;
using System.Linq;
using UnityEditor;
using System.Collections;

public class SpeechManager : MonoBehaviour
{

    public LipSync lipsyncComponent;
    public LipSyncData clip1;
    public LipSyncData clip2;
    public string inFolder = "Audio/AudioClip";
    public string path = "Assets/Audio/AudioClip/";

    // Use this for initialization
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + inFolder);
        FileInfo[] info = dir.GetFiles("*.asset");
        string[] fullNames = info.Select(f => f.FullName).ToArray();
        LipSyncData clip = null;
        foreach (string audioClipPath in fullNames)
        {
            string name = Path.GetFileName(audioClipPath);
            Debug.Log(audioClipPath);
            clip = (LipSyncData)AssetDatabase.LoadAssetAtPath(path + name, typeof(LipSyncData));
        }
        lipsyncComponent.Play(clip);
        // StartCoroutine(play(clip1));
        // StartCoroutine(play(clip2));
    }
    public IEnumerator play(LipSyncData clip)
    {
        if (clip != null)
        {
            lipsyncComponent.Play(clip);
            yield return new WaitForSeconds(clip.length);
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
