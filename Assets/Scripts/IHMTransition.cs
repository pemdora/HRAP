using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ce code permet de changer de scene entre les menus

public class IHMTransition : MonoBehaviour {

    //AsyncOperation ao;

	public void Transition_menu_1() //menu 1
	{
		ChangeScene("1-Main_menu");
	}
	public void Transition_menu_2()//menu 2
	{
		ChangeScene("2-Terms_of_contract");
	}
	public void Transition_menu_3()//menu 3
	{
		ChangeScene("3-Authentification");
	}
	public void Transition_menu_4()//menu 4
	{
        ChangeScene("4-Interview");
    }
	public void Transition_menu_5()//menu 5
	{
		ChangeScene("5-Greetings");
	}
	public void Transition_optionAudio()//menu 5
	{
		ChangeScene("6-Audio_Menu");
	}
	public void Finish()//exit
	{
		Application.Quit();
	}

	void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

		
}
