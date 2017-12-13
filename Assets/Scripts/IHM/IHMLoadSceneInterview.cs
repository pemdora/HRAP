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
        loader.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(slider.value);
	}
    public void LoadLevel4()
    {
        loader.gameObject.SetActive(true);
        cont.enabled = false;

        StartCoroutine(LoadLevelWithProgressBar("4-Interview"));

    }
    IEnumerator LoadLevelWithProgressBar(string sceneName)
    {
        Debug.Log("Start load scene");
        ao = SceneManager.LoadSceneAsync(sceneName);
        //ao.allowSceneActivation = false;
        
        while (!ao.isDone)
        {
            slider.value = ao.progress;
            //if (ao.progress == 0.9f)
            //{
            //    loadtext.text = "PRESS ENTER !";
            //    if (Input.GetKeyDown(KeyCode.Return))
            //    {
            //        ao.allowSceneActivation = true;
            //    }
            //}
            Debug.Log(ao.progress);
            yield return null;
        }
    }
}
