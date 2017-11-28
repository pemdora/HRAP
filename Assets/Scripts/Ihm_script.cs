using System.Collections.Generic;
using UnityEngine;
using HRAP;
public class Ihm_script : MonoBehaviour
{
    // I
    UIButton button_a, button_b, button_c, button_d;
    UILabel question, answer_a, answer_b, answer_c, answer_d;
    UILabel cname, clastname;
    string firstname = "";
    string lastname = "";
    P_Interview interview = new P_Interview("tom", "chef de projet");
    V_Question question_nbanswers_aswers;
    bool is_answered; // quizz state
    bool is_questioned; // once the question has been given = true
    string[] split_text;
    string base_text;

    // Use this for initialization
    void Start()
    {
        //Collection GUI Objects
        button_a = GameObject.Find("ButtonA").GetComponent<UIButton>();
        button_b = GameObject.Find("ButtonB").GetComponent<UIButton>();
        button_c = GameObject.Find("ButtonC").GetComponent<UIButton>();
        button_d = GameObject.Find("ButtonD").GetComponent<UIButton>();
        question = GameObject.Find("question").GetComponent<UILabel>();
        answer_a = GameObject.Find("answer_a").GetComponent<UILabel>();
        answer_b = GameObject.Find("answer_b").GetComponent<UILabel>();
        answer_c = GameObject.Find("answer_c").GetComponent<UILabel>();
        answer_d = GameObject.Find("answer_d").GetComponent<UILabel>();
        cname = GameObject.Find("cname").GetComponent<UILabel>();
        clastname = GameObject.Find("clastname").GetComponent<UILabel>();

        //Collecting variables from last scene ( authentication)
        if (Input_authentification.firstName != null && Input_authentification.lastName != null)
        {
            try
            {
                firstname = Input_authentification.firstName.value;
                lastname = Input_authentification.lastName.value;
            }
            catch (UnassignedReferenceException e)
            {
                Debug.Log(e.Message);
            }
        }
        //Initialising variables
        question.text = "[u][b]Question[/u] : [/b]";
        answer_a.text = "[FF0000][b]A: [/b][-]";
        answer_b.text = "[0000FF][b]B: [/b][-]";
        answer_c.text = "[00FF00][b]C: [/b][-]";
        answer_d.text = "[FFFF00][b]D: [/b][-]";
        question_nbanswers_aswers = interview.GetNextQuestion();
        is_answered = false;
        is_questioned = false;
        //settings buttons

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("1");
        //display name and lastname from last scene
        if (firstname != "" && lastname != "")
        {
            cname.text = Input_authentification.firstName.value;
            clastname.text = Input_authentification.lastName.value;
        }
        if (!is_questioned)
        {
            Debug.Log("2");
            Activate_buttons_nb_answers(question_nbanswers_aswers.NumAnswers);//activate buttons according to number of answers     
            question.text = question_nbanswers_aswers.Question;//print question
            List<UILabel> answers = List_answers_by_question(question_nbanswers_aswers.NumAnswers); // collect list of label for answers
            int index = 0;
            foreach (string a in question_nbanswers_aswers.Answers)//collect text answers
            {
                answers[index].text += a;
                index++;
            }
            is_questioned = true;
            is_answered = false;
        }
        else if (is_answered)
        {
            question.text = "[u][b]Question[/u] : [/b]";
            answer_a.text = "[FF0000][b]A: [/b][-]";
            answer_b.text = "[0000FF][b]B: [/b][-]";
            answer_c.text = "[00FF00][b]C: [/b][-]";
            answer_d.text = "[FFFF00][b]D: [/b][-]";
            Debug.Log("3");
            question_nbanswers_aswers = interview.GetNextQuestion();
            is_questioned = false;
            is_answered = false;
        }    
    }

    //button manager
    void Activate_buttons_nb_answers(int nb_answers)
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
           
        }catch(MissingReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }

    List<UILabel> List_answers_by_question(int nb_answers)
    {
        List<UILabel> result= new List<UILabel> (new UILabel[] { }); // init list of uilabel
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
    public void Button_is_pressed_next_question()
    {
        is_answered = true;
    }
}
