using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController5x5 : MonoBehaviour
{
    //Global variables
    public string GameStatus = "TileSelection";
    GameObject LevelComplete;
    
    GameObject SelectedTile;
    int[] SelectedTileCoords = new int[2];
    Vector2 SelectedTilePos;

    GameObject Vegetable;
    public GameObject[] CarrotsArray = new GameObject[2];
    int CarrotsRemaining = 2;
    
    GameObject HighlightSquare;
    public GameObject SpeechBubble;
    Text CarrotsRemainingText;
    GameObject QuestionPopUpManager;
    GameObject QuestionGenerator;
    GameObject BoardObject;

    // Start is called before the first frame update
    void Start()
    {
        //Make Highlight Square invisible
        HighlightSquare = GameObject.Find("HighlightSquare");
        HighlightSquare.SetActive(false);
        //Make Level Complete Sign invisible 
        LevelComplete = GameObject.Find("LevelComplete");
        LevelComplete.SetActive(false);

        QuestionPopUpManager = GameObject.Find("QuestionPopUp");
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().Start();
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().HideQuestionPopUp();
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().UpdateScoreText();

        //Make all vegetables invisible
        foreach (GameObject car in CarrotsArray)
        {
            car.SetActive(false);
        }

        //Make SpeechBubble visible
        SpeechBubble.SetActive(true);

        //Update vegetables remaining
        UpdateVegetablesRemaining();

        BoardObject = new GameObject();
        BoardObject.AddComponent<BoardModel>();
        BoardObject.GetComponent<BoardModel>().Level = 2;

        QuestionGenerator = new GameObject();
        QuestionGenerator.AddComponent<ArithmeticQuestionGenerator>();
        QuestionGenerator.GetComponent<ArithmeticQuestionGenerator>().Level = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatus == "InQuestion")
            OnQuestionInputChanged();
    }
        public void ReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //If GameStatus is TileSelection, on click highlight the selected tile and update GameStatus, SelectedTile and SelectedTileCoords
    public void SelectTile(GameObject obj)
    {
        //Check if GameStatus is TileSelection
        if (GameStatus == "TileSelection") 
        {
            //Set global SelectedTile to the selected tile and global SelectedTileCoords to the coordinates of the selected tile
            SelectedTile = obj;
            SelectedTileCoords = GetCoords(obj);

            //Get position of selected tile and set to position of HighlightSquare
            SelectedTilePos = obj.GetComponent<RectTransform>().anchoredPosition;
            HighlightSquare.GetComponent<RectTransform>().anchoredPosition = SelectedTilePos;

            //Make HighlightSquare visible
            HighlightSquare.SetActive(true);
            SpeechBubble.SetActive(false);

            //Change GameStatus to "InQuestion"
            GameStatus = "InQuestion";

            if (GameStatus == "InQuestion")
            {
                //Execute Question
                var question = QuestionGenerator.GetComponent<ArithmeticQuestionGenerator>().generateQuestion();
                QuestionPopUpManager.GetComponent<QuestionPopUpManager>().SetQuestion(question.Item1, question.Item2);
                StartCoroutine(QuestionPopUpManager.GetComponent<QuestionPopUpManager>().ShowQuestion()); 
            }
        }

    }
    //Supporting function: Converts vector coordinates of a GameObject into a 2D int array comprising X and Y coordinates in the simplified form [(0/1/2) , (0/1/2)]
    public int[] GetCoords(GameObject obj)
    {
        int[] objCoords = new int[2];
        
        //Obtain X and Y coordinates (Unity-form) of obj
        Vector2 objPos = obj.GetComponent<RectTransform>().anchoredPosition;
        float objPosX = objPos.x;
        float objPosY = objPos.y;

        //Convert X coordinate (Unity-form) of obj into simplified form and insert into objCoords
        switch (objPosX)
        {
            case -98:
                objCoords[0] = 0;
                break;
            case 2:
                objCoords[0] = 1;
                break;
            default:
                objCoords[0] = 2;
                break;
        }

        //Convert Y coordinate (Unity-form) of obj into simplified form and insert into objCoords
        switch (objPosY)
        {
            case 98:
                objCoords[1] = 0;
                break;
            case -2:
                objCoords[1] = 1;
                break;
            default:
                objCoords[1] = 2;
                break;
        }

        //Return the 2D array of simplified coordinates
        return objCoords;
    }
    public void OnQuestionInputChanged()
    {
        string userInput = QuestionPopUpManager.GetComponent<QuestionPopUpManager>().GetInputString();
        bool isCorrect = false;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isCorrect = QuestionPopUpManager.GetComponent<QuestionPopUpManager>().IsQuestionCorrect(userInput);

            if (isCorrect)
            {
                GameStatus = "QuestionCorrect";
                StartCoroutine(OnQuestionCorrect());
            }
        }
    }

    //Implementation of the Question() function
    IEnumerator OnQuestionCorrect()
    {
        //Wait 1 second
        yield return new WaitForSeconds(1);
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().HideQuestionPopUp();

        if (GameStatus == "QuestionCorrect")
        {
            QuestionPopUpManager.GetComponent<QuestionPopUpManager>().ResetQuestionInput();
            
            //Code to check if carrot is present
            string VegetableFound = BoardObject.GetComponent<BoardModel>().makeGuess(SelectedTileCoords[0], SelectedTileCoords[1]);

            //If carrot, then disappear tile and show carrot
            if (VegetableFound != "null")
            {
                //Dissapear tile
                SelectedTile.SetActive(false);

                if (VegetableFound == "Carrot")
                {
                    //Show find carrot, make visible and move to postion of tile
                    Vegetable = CarrotsArray[CarrotsRemaining - 1];
                    Vegetable.SetActive(true);
                    Vegetable.GetComponent<RectTransform>().anchoredPosition = SelectedTilePos;

                    //Update carrots remaining text
                    CarrotsRemaining -= 1;
                    UpdateVegetablesRemaining();

                    if (CarrotsRemaining == 0)
                    {
                        GameStatus = "LevelComplete";
                        //Wait 1 second
                        yield return new WaitForSeconds(1);
                        LevelComplete.SetActive(true);

                    }
                }
            }
            else //If no carrot, then disappear tile
            {
                SelectedTile.SetActive(false);
            }
        }
        if (GameStatus != "LevelComplete")
            GameStatus = "TileSelection";
    }

    //Update the vegetables remaining table using the global variables
    public void UpdateVegetablesRemaining()
    {
        CarrotsRemainingText = GameObject.Find("CarrotsRemainingText").GetComponent<Text>();
        CarrotsRemainingText.text = CarrotsRemaining.ToString();
    }

    public void HighlightSquareVisible(bool square)
    {
        if (square)
        {
            HighlightSquare.SetActive(true);
        }
        else
        {
            HighlightSquare.SetActive(false);
        }
    }

}
