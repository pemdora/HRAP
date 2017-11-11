using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ce code permet de changer de scene entre les menus

public class Menu_principal_start_button : MonoBehaviour {


	public void Transition_menu_1() //menu 1
	{
		changeScene("1-Main_menu");
	}
	public void Transition_menu_2()//menu 2
	{
		changeScene("2-Terms_of_contract");
	}
	public void Transition_menu_3()//menu 3
	{
		changeScene("3-Authentification");
	}
	public void Transition_menu_4()//menu 4
	{
		changeScene("4-Interview");
	}
	public void Transition_menu_5()//menu 5
	{
		changeScene("5-Greetings");
	}
	public void Transition_optionAudio()//menu 5
	{
		changeScene("6-Audio_Menu");
	}
	public void Finish()//exit
	{
		Application.Quit();
	}


	void changeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}
		
}
