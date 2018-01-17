using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class IHMLoadSceneInterview : MonoBehaviour {

    AsyncOperation ao;
    public UISlider slider;
    public UIPanel loader;
    UIButton cont;

    // Use this for initialization
    void Start () {
        cont = GameObject.Find("Bouton_continuer").GetComponent<UIButton>();
        //loader.gameObject.SetActive(false);
    }
	
    public void LoadLevel4()
    {
        loader.gameObject.SetActive(true);
        cont.isEnabled = false;

        StartCoroutine(LoadLevelWithProgressBar("4-Interview"));

    }
    IEnumerator LoadLevelWithProgressBar(string sceneName)
    {
        Debug.Log("Start load scene");
        ao = SceneManager.LoadSceneAsync(sceneName);
        
        while (!ao.isDone)
        {
            slider.value = ao.progress;
            yield return null;
        }
    }
}
