using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestionPopUpManager : MonoBehaviour
{
    public GameObject QuestionPopUpUI;
    public InputField QuestionInputUI;
    GameObject HighlightSquare;  
    GameObject QuestionTitle;
    GameObject ScoreManager;
    public Text ScoreCountUI;
    double correctAnswer;
    GameObject AnswerNotificationUI;
    GameObject QuestionGen;
    public GameObject CorrectRing;
    public GameObject IncorrectRing;

    // Start is called before the first frame update
    void Start()
    {
        HighlightSquare = GameObject.Find("HighlightSquare");
        QuestionTitle = GameObject.Find("QuestionTitle");
        AnswerNotificationUI = GameObject.Find("AnswerNotification");
        ScoreManager = new GameObject();
        ScoreManager.name = "score";
        ScoreManager.AddComponent<UserScore>();
        QuestionGen = new GameObject();
        QuestionGen.AddComponent<ArithmeticQuestionGenerator>();
        QuestionGen.GetComponent<ArithmeticQuestionGenerator>().Level = 1;
    }

    public void HideQuestionPopUp()
    {
        //Make QuestionPopUp invisible
        QuestionPopUpUI.SetActive(false);
    }

    public void HideAnswerNotification()
    {
        //Make AnswerNotification invisible
        AnswerNotificationUI.SetActive(false);
    }

    public void ResetQuestion()
    {
        //reset question once button is clicked
        Tuple<string, double> questionTuple = QuestionGen.GetComponent<ArithmeticQuestionGenerator>().generateQuestion();
        SetQuestion(questionTuple.Item1, questionTuple.Item2);
        IncorrectRing.SetActive(false);
        CorrectRing.SetActive(false);
        QuestionInputUI.GetComponent<InputField>().text = "";
    } 
    public void SetQuestion(string questionText, double questionAnswer)
    {   
        QuestionTitle.GetComponent<Text>().text = questionText;
        correctAnswer = questionAnswer;
    }

    public bool IsQuestionCorrect(string userAnswer)
    {   
        if (isNumeric(userAnswer))
        {   
            if (System.Convert.ToDouble(userAnswer) == correctAnswer) //Question Correct
            {
                CorrectRing.SetActive(true);
                ScoreManager.GetComponent<UserScore>().incrementScore(); //100 points if answered correctly first try 
                UpdateScoreText();
                HideAnswerNotification();
                return true;
            }
            else //Question Incorrect
            {
                IncorrectRing.SetActive(true);
                ScoreManager.GetComponent<UserScore>().halveScore(); //Next available points are halved 
                AnswerNotificationUI.SetActive(true);
                return false;
            }
        }
        else //Answer not a number
        {
            IncorrectRing.SetActive(true);
            ScoreManager.GetComponent<UserScore>().halveScore(); //Next available points are halved 
            AnswerNotificationUI.SetActive(true); 
            return false;
        }
    }

    void UpdateScoreText()
    {
        double score  = ScoreManager.GetComponent<UserScore>().CurrentScore; 
        score = Convert.ToInt32(score);
        ScoreCountUI = GameObject.Find("ScoreTextUI").GetComponent<Text>();
        ScoreCountUI.text = score.ToString();

    }

    bool isNumeric(String str) 
    {
        if (str == null) 
            return false;

        int number;  
        float value;   
        if (int.TryParse(str, out number))
            return true;
        else if (float.TryParse(str, out value))
            return true;
        else
            return false;
    }


    //For the purposes of the Week 7 demo, to show the highlight mechanic, Question() waits and then implements a correct answer
    public IEnumerator ShowQuestion()
    {
        //Wait 1 second
        yield return new WaitForSeconds(1);
        QuestionPopUpUI.SetActive(true);
        //Disappear HighlightSquare
        HighlightSquare.SetActive(false);
        HideAnswerNotification();
        IncorrectRing.SetActive(false);
        CorrectRing.SetActive(false);
    }

    public void ResetQuestionInput()
    {
        QuestionInputUI.GetComponent<InputField>().text = "";
    }

    public string GetInputString()
    {
        InputField inputField = QuestionInputUI.GetComponent<InputField>();
        string value = inputField.text;

        return value;
    }
}
