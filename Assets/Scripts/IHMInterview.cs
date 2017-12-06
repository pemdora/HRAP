using System.Collections.Generic;
using UnityEngine;
using HRAP;
using System;
using System.Collections;
public class IHMInterview : MonoBehaviour
{
    // Init
    UIButton button_a, button_b, button_c, button_d, button_pause, button_settings;
    UILabel question, answer_a, answer_b, answer_c, answer_d,comment;
    UILabel cname, clastname, cposition;
    P_Interview interview;
    //IHMAuthentification authentication;
    // Use this for initialization
    void Start()
    {
        //Collection GUI Objects
        button_a = GameObject.Find("ButtonA").GetComponent<UIButton>();
        button_b = GameObject.Find("ButtonB").GetComponent<UIButton>();
        button_c = GameObject.Find("ButtonC").GetComponent<UIButton>();
        button_d = GameObject.Find("ButtonD").GetComponent<UIButton>();
        button_settings = GameObject.Find("Button_settings").GetComponent<UIButton>();
        button_pause = GameObject.Find("Button_pause").GetComponent<UIButton>();

        question = GameObject.Find("question").GetComponent<UILabel>();
        answer_a = GameObject.Find("answer_a").GetComponent<UILabel>();
        answer_b = GameObject.Find("answer_b").GetComponent<UILabel>();
        answer_c = GameObject.Find("answer_c").GetComponent<UILabel>();
        answer_d = GameObject.Find("answer_d").GetComponent<UILabel>();
        comment = GameObject.Find("comment").GetComponent<UILabel>();

        cname = GameObject.Find("cname").GetComponent<UILabel>();
        clastname = GameObject.Find("clastname").GetComponent<UILabel>();
        cposition = GameObject.Find("cposition").GetComponent<UILabel>();

        //Collecting variables from last scene ( authentication)
        try
        {
            if (IHMAuthentification.firstName.value != null && IHMAuthentification.lastName.value != null && IHMAuthentification.poplist_label.text != "Choix du poste")
            {
                cname.text = IHMAuthentification.firstName.value;
                clastname.text = IHMAuthentification.lastName.value;
                cposition.text = IHMAuthentification.poplist_label.text;
            }
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log(e.Message);
        }
        //Initialising variables
        question.text = "[u][b]Question[/u] : [/b]";
        answer_a.text = "[FF0000][b]A: [/b][-]";
        answer_b.text = "[0000FF][b]B: [/b][-]";
        answer_c.text = "[00FF00][b]C: [/b][-]";
        answer_d.text = "[FFFF00][b]D: [/b][-]";
        // question_nbanswers_aswers = interview.GetNextQuestion();
        //is_answered = false;
        // is_questioned = false;


    }

    // Initialise Presenter
    public void SetPresenter(P_Interview interview)
    {
        this.interview = interview;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //button manager
    public void Activate_buttons_nb_answers(int nb_answers)
    {
        try
        {
            if (nb_answers <= 4 && nb_answers >= 2)
            {
                switch (nb_answers)
                {
                    case 2:
                        Enable_2_buttons();
                        break;
                    case 3:
                        Enable_3_buttons();
                        break;
                    case 4:
                        Enable_all_buttons();
                        break;
                    default:
                        Desable_all_buttons();
                        break;
                }
            }

        }
        catch (MissingReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }

    List<UILabel> List_answers_by_question(int nb_answers)
    {
        List<UILabel> result = new List<UILabel>(new UILabel[] { }); // init list of uilabel
        switch (nb_answers)
        {
            case 2:
                result.Add(answer_a);
                result.Add(answer_b);
                break;
            case 3:
                result.Add(answer_a);
                result.Add(answer_b);
                result.Add(answer_c);
                break;
            case 4:
                result.Add(answer_a);
                result.Add(answer_b);
                result.Add(answer_c);
                result.Add(answer_d);
                break;
            default:
                result = null;
                break;
        }
        return result;
    }
    void Enable_2_buttons()
    {
        button_a.isEnabled = true;
        button_b.isEnabled = true;
        button_c.isEnabled = false;
        button_d.isEnabled = false;

        answer_a.enabled = true;
        answer_b.enabled = true;
        answer_c.enabled = false;
        answer_d.enabled = false;


    }
    void Enable_3_buttons()
    {
        button_a.isEnabled = true;
        button_b.isEnabled = true;
        button_c.isEnabled = true;
        button_d.isEnabled = false;

        answer_a.enabled = true;
        answer_b.enabled = true;
        answer_c.enabled = true;
        answer_d.enabled = false;
    }
    void Enable_all_buttons()
    {
        button_a.isEnabled = true;
        button_b.isEnabled = true;
        button_c.isEnabled = true;
        button_d.isEnabled = true;

        answer_a.enabled = true;
        answer_b.enabled = true;
        answer_c.enabled = true;
        answer_d.enabled = true;
    }
    void Desable_all_buttons()
    {
        button_a.isEnabled = false;
        button_b.isEnabled = false;
        button_c.isEnabled = false;
        button_d.isEnabled = false;

        answer_a.enabled = false;
        answer_b.enabled = false;
        answer_c.enabled = false;
        answer_d.enabled = false;
    }
    //n'est plus utilisé depuis l'installation du controlleur
    public void Button_is_pressed_next_question()
    {
        // Clean previous questions and answers
        question.text = "[u][b]Question[/u] : [/b]";
        answer_a.text = "[FF0000][b]A: [/b][-]";
        answer_b.text = "[0000FF][b]B: [/b][-]";
        answer_c.text = "[00FF00][b]C: [/b][-]";
        answer_d.text = "[FFFF00][b]D: [/b][-]";

        //is_answered = true;
        interview.SetChosenAnswer(0);
    }

    public void DisplayQuestion(string q)//for controller
    {
        question.text = "[u][b]Question[/u] : [/b]";
        question.text += q;//print question
    }
    public void DisplayAnswers(List<string> ans)//for controller
    {
        // collect list of label for answers
        Debug.Log(ans.Count);
        List<UILabel> answers = List_answers_by_question(ans.Count);

        Debug.Log(ans.Count);
        int index = 0;
        foreach (string a in ans)//collect text answers
        {
            answers[index].text += a;
            index++;
        }
    }
    public void DisplayComment(string c)//for controller
    {
        comment.text +="\n\n"+c;//print question
    }
    public void Clear()
    {
        question.text = "WAITING FOR NEXT QUESTION";
    }
    public void Pause()
    {
       
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                }
                else
                    Time.timeScale = 1;
    }
}
