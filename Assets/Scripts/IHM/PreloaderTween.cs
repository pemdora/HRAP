/* 
PreloaderTween.cs by ThunderWire_Games © All Rights Reserved﻿ 2014
This script is for PreloadSceneProgressBar.cs for Tween Preloader when is Level Loaded
!!! You must Have NGUI Latest Version for this script Work !!!
*/

using UnityEngine;
using System.Collections;

public class PreloaderTween : MonoBehaviour {
	public GameObject FadeBackground;
	public GameObject FadeMapText;
	
	public GameObject FadeProgressBar;
	public GameObject FadeProgressForeground;
	public GameObject FadeProgressThumb;
	public GameObject FadeProgressLabel;

	// Use this for initialization
	public void StartTween () {		
				// Main Tweens ( Start )
				// !Fade BG! 				
                TweenColor.Begin(FadeBackground,1,new Color(0,0,0,0));
				// !Fade Map Text! 
                TweenColor.Begin(FadeMapText,1,new Color(0,0,0,0));

//				##### FADE ProgressBars ##### 
				
				// !FadeProgressBar!
                TweenColor.Begin(FadeProgressBar,1,new Color(0,0,0,0));
				// !FadeProgressForeground!
                TweenColor.Begin(FadeProgressForeground,1,new Color(0,0,0,0));
				// !FadeProgressThumb! 
                TweenColor.Begin(FadeProgressThumb,1,new Color(0,0,0,0));
				// !FadeProgressLabel! 
                TweenColor.Begin(FadeProgressLabel,1,new Color(0,0,0,0));
				// Main Tweens ( End  )
				PreloadSceneProgressBar MainProgressBar = this.GetComponent<PreloadSceneProgressBar>();
				MainProgressBar.Tweened = true;
	}
	
}
