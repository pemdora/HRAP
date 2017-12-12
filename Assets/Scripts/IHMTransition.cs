using UnityEngine;
using UnityEngine.SceneManagement;

//Ce code permet de changer de scene entre les menus

public class IHMTransition : MonoBehaviour {


	public static void Transition_menu_1() //menu 1
	{
		ChangeScene("1-Main_menu");
	}
	public static void Transition_menu_2()//menu 2
	{
		ChangeScene("2-Terms_of_contract");
	}
	public static void Transition_menu_3()//menu 3
	{
		ChangeScene("3-Authentification");
	}
	public static void Transition_menu_4()//menu 4
	{
        ChangeScene("4-Interview");
    }
	public static void Transition_menu_5()//menu 5
	{
		ChangeScene("5-Greetings");
	}
	public static void Transition_optionAudio()//menu 5
	{
		ChangeScene("6-Audio_Menu");
	}
	public static void Finish()//exit
	{
		Application.Quit();
	}

    static void ChangeScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

		
}
