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
    Vector3 SelectedTilePos;
    public GameObject[] TileRow1 = new GameObject[5];
    public GameObject[] TileRow2 = new GameObject[5];
    public GameObject[] TileRow3 = new GameObject[5];
    public GameObject[] TileRow4 = new GameObject[5];
    public GameObject[] TileRow5 = new GameObject[5];

    GameObject Vegetable;
    public GameObject[] CarrotsArray = new GameObject[2];
    int CarrotsRemaining = 2;
    public GameObject[] BananaArray = new GameObject[2];
    int BananasRemaining = 2;
    
    GameObject HighlightSquare;
    public GameObject SpeechBubble;
    public GameObject SpeechText;
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
        foreach (GameObject ban in BananaArray)
        {
            ban.SetActive(false);
        }

        Text text = SpeechText.GetComponent<Text>();
        text.text = SteveQuotes.TileSelection;
        StartCoroutine(FadeSpeechBubble());

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
            //Get position of selected tile and set to position of HighlightSquare
            SelectedTilePos = SelectedTile.transform.position;
            SelectedTilePos.z = 0;
            HighlightSquare.transform.position = SelectedTilePos;

            //Make HighlightSquare visible
            HighlightSquare.SetActive(true);
            SpeechBubble.SetActive(false);

            SelectedTileCoords = GetCoords(obj);

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

        for (int i = 0; i < 5; i++)
        {
            if (obj == TileRow1[i])
            {
                objCoords[0] = i;
                objCoords[1] = 0;     
            }
            else if (obj == TileRow2[i])
            {
                objCoords[0] = i;
                objCoords[1] = 1;     
            }
            else if (obj == TileRow3[i])
            {
                objCoords[0] = i;
                objCoords[1] = 2;     
            }
            else if (obj == TileRow4[i])
            {
                objCoords[0] = i;
                objCoords[1] = 3;     
            }
            else if (obj == TileRow5[i])
            {
                objCoords[0] = i;
                objCoords[1] = 4;     
            }
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
                    Vegetable.transform.position = SelectedTilePos;

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

                    Text text = SpeechText.GetComponent<Text>();
                    text.text = SteveQuotes.CarrotFound;
                    StartCoroutine(FadeSpeechBubble());
                }
                else if (VegetableFound == "Banana" && BananasRemaining > 0)
                {
                    //Show find banana, make visible and move to postion of tile
                    GameObject Fruit = BananaArray[BananasRemaining - 1];
                    Fruit.SetActive(true);
                    Fruit.transform.position = SelectedTilePos;
                    BananasRemaining -= 1;
                    BoardObject.GetComponent<BoardModel>().PushCarrotsBackOneMove();
                    BoardObject.GetComponent<BoardModel>().GetCarrotPosition();
                    Text text = SpeechText.GetComponent<Text>();
                    text.text = SteveQuotes.BananaFound;
                    StartCoroutine(FadeSpeechBubble());
                }
                }
            }
            else //If no carrot, then disappear tile
            {
                SelectedTile.SetActive(false);
                BoardObject.GetComponent<BoardModel>().moveVegetables();
                BoardObject.GetComponent<BoardModel>().GetCarrotPosition();
                Text text = SpeechText.GetComponent<Text>();
                text.text = SteveQuotes.TileEmpty;
                StartCoroutine(FadeSpeechBubble());
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
        IEnumerator FadeSpeechBubble()
    {
        SpeechBubble.SetActive(true);
        yield return new WaitForSeconds(1);

        SpeechBubble.GetComponent<Image>().CrossFadeAlpha(1.0f, 0f, false); //fade in
        SpeechText.GetComponent<Text>().CrossFadeAlpha(1.0f, 0f, false); //fade in
        SpeechBubble.GetComponent<Image>().CrossFadeAlpha(0.0f, 2.5f, false); //fade out
        SpeechText.GetComponent<Text>().CrossFadeAlpha(0.0f, 2.5f, false); //fade out

    }

}
