using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPopUpManager : MonoBehaviour
{
    public GameObject QuestionPopUpUI;
    public InputField QuestionInputUI;
    GameObject HighlightSquare;

    // Start is called before the first frame update
    void Start()
    {
        HighlightSquare = GameObject.Find("HighlightSquare");
    }

    public void HideQuestionPopUp()
    {
        //Make QuestionPopUp invisible
        QuestionPopUpUI.SetActive(false);
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
