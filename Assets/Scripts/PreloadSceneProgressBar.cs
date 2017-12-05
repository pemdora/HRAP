/* 
PreloadSceneProgressBar.cs by ThunderWire_Games © All Rights Reserved﻿ 2014
This script is for Loading Scene in Other Scene =? For Preloading Scene
!!! You must Have NGUI Latest Version for this script Work !!!
*/

using UnityEngine;
using System.Collections;


public class PreloadSceneProgressBar : MonoBehaviour {
	
	private AsyncOperation async;
	private float loadProgress = 0;
	
	public GameObject _ProgressBar;
	public string _LevelToLoad; // !! If use PlayerPrefs you must keep this blank
	public bool UsePlayerPrefs; // !! If you use this you must Set next Scene by this: PlayerPrefs.SetString("LoadLevel", NextSceneName);
	
	public bool PreloadHigh;
	public bool PreloadLow;
	
	public bool StartPreload;
	[HideInInspector]
	public bool Tweened;

    UIButton continu;
	
	
	// ---------------------------------------
void Start () {
        continu = GameObject.Find("Bouton_continuer").GetComponent<UIButton>();
	if( UsePlayerPrefs ){
	_LevelToLoad = PlayerPrefs.GetString("LoadLevel");
}
	if(PreloadHigh){
		Application.backgroundLoadingPriority = ThreadPriority.High; // Good for fast loading when showing progress bars
	}
	if(PreloadLow){
		Application.backgroundLoadingPriority = ThreadPriority.Low; // Good for loading in the background while the game is playing
	}
	if(StartPreload){
		StartCoroutine(LevelLoaderProcess());
	}
}	
	// --------------------------------------- 
	
	  /* MAIN Preload Process */
IEnumerator LevelLoaderProcess()
{
        PreloaderTween Tweener = this.GetComponent<PreloaderTween>();
        Debug.Log ("Loading Start");
        
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_LevelToLoad);
        async.allowSceneActivation = false;

        // Wait until finished and get progress

        while( !async.isDone )
        {
                loadProgress = async.progress;

                if(loadProgress >= 0.9f)
        {
		loadProgress = 1.0f;
                // Almost done
                break;
        }

        yield return null;
        }

        // Start new scene and Tween
	yield return new WaitForSeconds(1.0f);
	Tweener.StartTween();
	if(Tweened){
		yield return new WaitForSeconds(1.0f);
        async.allowSceneActivation = true;
        yield return async;
	}
}
    // ---------------------------------------
	void Update () {
	if(StartPreload){
		Debug.Log ("Level[" + _LevelToLoad + "] Load Progress : " + loadProgress*100);
		UISlider ProgressBar = _ProgressBar.GetComponent<UISlider>();
        ProgressBar.value = loadProgress;
	}
	}

}