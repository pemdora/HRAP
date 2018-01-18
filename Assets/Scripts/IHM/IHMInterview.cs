using System.Collections.Generic;
using UnityEngine;
using HRAP;
using System;
using System.Collections;
public class IHMInterview : MonoBehaviour
{
    // Init
    UIButton button_a, button_b, button_c, button_d, buttonNext/*, button_pause, button_settings, yesContinue, noContinue*/;
    UILabel question, answer_a, answer_b, answer_c, answer_d, comment;
    static UILabel cname, clastname, cposition;
    P_Interview interview;
    GameObject continuePanel, pause, finishPanel;
    UIScrollView scrollview;
    //collect element from scene
    static GameObject UIroot;
    GameObject quizz;
    GameObject Comment_scroll_area;
    enum answers { A, B, C, D };
    IHMTransition ihmtrans;//use for the finish

    // Use this for initialization
    void Start()
    {

        //Collection GUI Objects
        button_a = GameObject.Find("ButtonA").GetComponent<UIButton>();
        button_b = GameObject.Find("ButtonB").GetComponent<UIButton>();
        button_c = GameObject.Find("ButtonC").GetComponent<UIButton>();
        button_d = GameObject.Find("ButtonD").GetComponent<UIButton>();
        buttonNext = GameObject.Find("ButtonNext").GetComponent<UIButton>();

        question = GameObject.Find("question").GetComponent<UILabel>();
        answer_a = GameObject.Find("answer_a").GetComponent<UILabel>();
        answer_b = GameObject.Find("answer_b").GetComponent<UILabel>();
        answer_c = GameObject.Find("answer_c").GetComponent<UILabel>();
        answer_d = GameObject.Find("answer_d").GetComponent<UILabel>();
        comment = GameObject.Find("comment").GetComponent<UILabel>();

        pause = GameObject.Find("Pause");
        finishPanel = GameObject.Find("FinishInterviewPanel");
        //continuePanel = GameObject.Find("PanelDisplayContinueInterview");
        //yesContinue = GameObject.Find("ButtonYes").GetComponent<UIButton>();
        //noContinue = GameObject.Find("ButtonNo").GetComponent<UIButton>();


        scrollview = GameObject.Find("Comment_scroll_area").GetComponent<UIScrollView>();
        UIroot = GameObject.Find("UI Root"); // use to mask all ngui elements
        quizz = GameObject.Find("Quizz");
        Comment_scroll_area = GameObject.Find("Comment_scroll_area");
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
        catch (NullReferenceException e)
        {
            Debug.Log(e.Message);
        }
        //Initialising variables

        question.text = "[u][b]Question[/u] : [/b]";
        answer_a.text = "[FF0000][b]A: [/b][-]";
        answer_b.text = "[0000FF][b]B: [/b][-]";
        answer_c.text = "[00FF00][b]C: [/b][-]";
        answer_d.text = "[FFFF00][b]D: [/b][-]";

        pause.SetActive(false);
        //continuePanel.SetActive(false);
        finishPanel.SetActive(false);
        buttonNext.gameObject.SetActive(false);

    }
    void update()
    {
    }
    // Initialise Presenter
    public void SetPresenter(P_Interview interview)
    {
        this.interview = interview;
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
        buttonNext.gameObject.SetActive(false);
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
        buttonNext.gameObject.SetActive(true);

        answer_a.enabled = false;
        answer_b.enabled = false;
        answer_c.enabled = false;
        answer_d.enabled = false;
    }
    public void Button_is_pressed_next_question()
    {

        if (UIButton.current == button_a)
        {
            interview.SetChosenAnswer((int)answers.A);
            //Debug.Log((int)answers.A);
        }
        else if (UIButton.current == button_b)
        {
            interview.SetChosenAnswer((int)answers.B);

        }
        else if (UIButton.current == button_c)
        {
            interview.SetChosenAnswer((int)answers.C);

        }
        else if (UIButton.current == button_d)
        {
            interview.SetChosenAnswer((int)answers.D);

        }
        else if (UIButton.current == buttonNext)
        {
            interview.SetChosenAnswer(0);
        }
        else
        {
            Debug.LogError("ERROR ! ");
        }


        // Clean previous questions and answers
        question.text = "[u][b]Question[/u] : [/b]";
        answer_a.text = "[FF0000][b]A: [/b][-]";
        answer_b.text = "[0000FF][b]B: [/b][-]";
        answer_c.text = "[00FF00][b]C: [/b][-]";
        answer_d.text = "[FFFF00][b]D: [/b][-]";
    }

    public void DisplayQuestion(string q)//for controller
    {
        question.text = "[u][b]Question[/u] : [/b]";
        question.text += q;//print question
    }
    public void DisplayAnswers(List<string> ans)//for controller
    {
        // collect list of label for answers
        List<UILabel> answers = List_answers_by_question(ans.Count);

        int index = 0;
        foreach (string a in ans)//collect text answers
        {
            answers[index].text += a;
            index++;
        }
    }
    public void DisplayComment(string c)//for controller
    {
        comment.text += "\n\n" + c;//print question
        scrollview.UpdateScrollbars();
        scrollview.verticalScrollBar.value = 1;
        buttonNext.gameObject.SetActive(true);
    }
    public void Clear()
    {
        question.text = "WAITING FOR NEXT QUESTION";
        answer_a.text = "";
        answer_b.text = "";
        answer_c.text = "";
        answer_d.text = "";
        buttonNext.gameObject.SetActive(true);
    }
    public void Pause()
    {


        if (Time.timeScale == 1)
        {
            MaskMainNguiComponent(false);
            pause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            MaskMainNguiComponent(true);
            pause.SetActive(false);
            Time.timeScale = 1;
        }

    }
    //THIS CODE IS AN IMPROVEMENT NOT USED FOR THE RELEASE 
    //public void DisplayContinuePanel()
    //{

    //   // continuePanel.SetActive(true);

    //    if (UIButton.current == yesContinue)
    //    {
    //        continuePanel.SetActive(false);
    //        Debug.Log("Close de continue window");
    //    }
    //    else if (UIButton.current == noContinue)
    //    {
    //        ihmtrans.Transition_menu_5();
    //        Debug.Log("Interview finished");
    //    }

    //}
    public void MaskMainNguiComponent(bool x)
    {
        quizz.SetActive(x);
        Comment_scroll_area.SetActive(x);
    }
    public static void MaskAllNguiComponents(bool x)
    {
        UIroot.SetActive(x);
    }
    public void Over()
    {
        question.text = "THE INTERVIEW IS OVER";
        finishPanel.SetActive(true);
    }
    public string GetName()
    {
        if (cname.text != "Name") return cname.text;
        return "";
    }

    public string GetPosition()
    {
        if (cposition.text != "Position") return cposition.text;
        return "";
    }
}
