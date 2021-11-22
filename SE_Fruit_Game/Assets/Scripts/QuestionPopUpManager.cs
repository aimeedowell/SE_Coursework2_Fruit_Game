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
    double correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        HighlightSquare = GameObject.Find("HighlightSquare");
        QuestionTitle = GameObject.Find("QuestionTitle");
    }

    public void HideQuestionPopUp()
    {
        //Make QuestionPopUp invisible
        QuestionPopUpUI.SetActive(false);
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
            if (System.Convert.ToDouble(userAnswer) == correctAnswer)
                return true;
            else 
                return false;
        }
        else
            return false;
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
