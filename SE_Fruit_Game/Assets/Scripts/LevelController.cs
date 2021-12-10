using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    GameObject LevelComplete;
    GameObject LevelFailed;
    GameObject MidLevelMenu;
    
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
    public GameObject[] BroccoliArray = new GameObject[1];
    public GameObject[] BananaArray = new GameObject[2];
    int CarrotsRemaining = 2;
    int BroccoliRemaining = 1;
    int BananasRemaining = 2;
    
    GameObject HighlightSquare;
    public GameObject SpeechBubble;
    public GameObject SpeechText;
    Text CarrotsRemainingText;
    Text BroccoliRemainingText;
    GameObject QuestionPopUpManager;
    GameObject QuestionGenerator;
    GameObject BoardObject;

    int startingScore;
    int level;
   
    public void ContinueButtonClicked()
    {

        Debug.Log(level);

        if (level == 1)
        {
            SceneManager.LoadScene("MathsLevel_2");     
        }
        else if (level == 2)
        {
            //SceneManager.LoadScene("MathsLevel_3"); // Move to Level 3
        }

        level += 1;
        StaticVariables.Level = level;
    }

    public void RetryButtonClicked()
    {
        Reset();
        MidLevelMenu.SetActive(false);
    }
    public void ExitButtonClicked()
    {
       if (StaticVariables.GameStatus !="LevelComplete" && StaticVariables.GameStatus != "LevelFailed")
       {
            MidLevelMenu.SetActive(true);
            StaticVariables.GameStatus = "Paused";
       }
    }
    public void ResumeButtonClicked()
    {
       MidLevelMenu.SetActive(false);
       StaticVariables.GameStatus = "TileSelection";
    }

    public void ReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Reset();
    }

    private void Awake() 
    {

        StaticVariables.StartingLevel = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Make Highlight Square invisible
        HighlightSquare = GameObject.Find("HighlightSquare");
        HighlightSquare.SetActive(false);
        //Make Level Complete Sign invisible 
        LevelComplete = GameObject.Find("LevelComplete");
        LevelComplete.SetActive(false);
        //Make Level Failed Sign invisible 
        LevelFailed = GameObject.Find("LevelFailed");
        LevelFailed.SetActive(false);
        //Make MidLevelMenu invisible 
        MidLevelMenu = GameObject.Find("MidLevelMenu");
        MidLevelMenu.SetActive(false);

        QuestionPopUpManager = GameObject.Find("QuestionPopUp");
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().Start();
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().HideQuestionPopUp();
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().UpdateScoreText();

        StaticVariables.GameStatus = "TileSelection";
        
        if (StaticVariables.Level > StaticVariables.StartingLevel)
            level = StaticVariables.Level;
        else
            level = StaticVariables.StartingLevel;


        //Make all vegetables invisible
        foreach (GameObject car in CarrotsArray)
        {
            car.SetActive(false);
        }
        if (level > 1)
        {
            foreach (GameObject ban in BananaArray)
            {
                ban.SetActive(false);
            }
            foreach (GameObject bro in BroccoliArray)
            {
                bro.SetActive(false);
            }
        }
  

        Text text = SpeechText.GetComponent<Text>();
        text.text = SteveQuotes.TileSelection;
        StartCoroutine(FadeSpeechBubble());

        //Update vegetables remaining
        UpdateVegetablesRemaining();

        BoardObject = new GameObject();
        BoardObject.AddComponent<BoardModel>();
        BoardObject.GetComponent<BoardModel>().Level = level;

        QuestionGenerator = new GameObject();
        QuestionGenerator.AddComponent<ArithmeticQuestionGenerator>();
        QuestionGenerator.GetComponent<ArithmeticQuestionGenerator>().Level = level;

        startingScore = StaticVariables.Score;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticVariables.GameStatus == "InQuestion")
            OnQuestionInputChanged();
    }
    //If GameStatus is TileSelection, on click highlight the selected tile and update GameStatus, SelectedTile and SelectedTileCoords
    public void SelectTile(GameObject obj)
    {
        //Check if GameStatus is TileSelection
        if (StaticVariables.GameStatus == "TileSelection") 
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
            StaticVariables.GameStatus = "InQuestion";

            if (StaticVariables.GameStatus == "InQuestion")
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

        if (level == 1)
        {
            for (int i = 0; i < 3; i++)
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
            }
        }
        else
        {
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
                StaticVariables.GameStatus = "QuestionCorrect";
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

        if (StaticVariables.GameStatus == "QuestionCorrect")
        {
            QuestionPopUpManager.GetComponent<QuestionPopUpManager>().ResetQuestionInput();
            
            //Code to check if carrot is present
            string VegetableFound = BoardObject.GetComponent<BoardModel>().makeGuess(SelectedTileCoords[0], SelectedTileCoords[1]);

            //If carrot, then disappear tile and show carrot
            if (VegetableFound != "null")
            {
                //Dissapear tile
                SelectedTile.SetActive(false);

                if (VegetableFound == "Broccoli")
                    RemoveBroccoliFromBoard();
                else if (VegetableFound == "Carrot")
                    RemoveCarrotFromBoard();
                else if (VegetableFound == "Banana" && BananasRemaining > 0)
                    PushVegetablesBack();

                if (level == 1)
                {
                    if (CarrotsRemaining == 0)
                        StartCoroutine(ShowLevelComplete());
                }
                else
                {
                    if (BroccoliRemaining == 0 && CarrotsRemaining == 0)
                        StartCoroutine(ShowLevelComplete());
                }
            }
            else //If no vegetable, then disappear tile
            {
                SelectedTile.SetActive(false);
                if (level > 1 )
                {
                    BoardObject.GetComponent<BoardModel>().moveVegetables();
                    BoardObject.GetComponent<BoardModel>().GetCarrotPosition();
                }
                if (level > 1 && BoardObject.GetComponent<BoardModel>().HasLevelFailed())
                {
                    ShowLevelFailed();
                }
                Text text = SpeechText.GetComponent<Text>();
                text.text = SteveQuotes.TileEmpty;
                StartCoroutine(FadeSpeechBubble());
            }
        }
        if (StaticVariables.GameStatus != "LevelComplete" || StaticVariables.GameStatus != "LevelFailed")
            StaticVariables.GameStatus = "TileSelection";
    }

    //Update the vegetables remaining table using the global variables
    public void UpdateVegetablesRemaining()
    {
        CarrotsRemainingText = GameObject.Find("CarrotsRemainingText").GetComponent<Text>();
        CarrotsRemainingText.text = CarrotsRemaining.ToString();
        if (level > 1)
        {
            BroccoliRemainingText = GameObject.Find("BroccoliRemainingText").GetComponent<Text>();
            BroccoliRemainingText.text = BroccoliRemaining.ToString();
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

    void Reset() 
    {
        StaticVariables.GameStatus = "TileSelection";
        CarrotsRemaining = 2;
        BananasRemaining = 2;
        BroccoliRemaining = 1;
        StaticVariables.Score = startingScore;
        //Make Highlight Square invisible
        HighlightSquare.SetActive(false);
        //Make Level Complete Sign invisible 
        LevelComplete.SetActive(false);
        //Make Level Failed Sign invisible 
        LevelFailed.SetActive(false);

        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().HideQuestionPopUp();
        QuestionPopUpManager.GetComponent<QuestionPopUpManager>().UpdateScoreText();

        level = StaticVariables.Level;

        //Make all vegetables invisible
        foreach (GameObject car in CarrotsArray)
        {
            car.SetActive(false);
        }
        if (level > 1)
        {
            foreach (GameObject ban in BananaArray)
            {
                ban.SetActive(false);
            }
            foreach (GameObject broc in BroccoliArray)
            {
                broc.SetActive(false);
            }
        }

        if (level == 1)
        {
            for (int i = 0; i < 3; i++ )
            {
                TileRow1[i].SetActive(true);
                if (i != 1) // Avoid empty obj for strawberry
                    TileRow2[i].SetActive(true);
                TileRow3[i].SetActive(true);
            }
        }
        else if (level == 2)
        {
            for (int i = 0; i < 5; i++ )
            {
                TileRow1[i].SetActive(true);
                TileRow2[i].SetActive(true);
                if (i != 2) // Avoid empty obj for strawberry
                    TileRow3[i].SetActive(true);
                TileRow4[i].SetActive(true);
                TileRow5[i].SetActive(true);
    
            }
        }
 

        Text text = SpeechText.GetComponent<Text>();
        text.text = SteveQuotes.TileSelection;
        StartCoroutine(FadeSpeechBubble());

        BoardObject = new GameObject();
        BoardObject.AddComponent<BoardModel>();
        BoardObject.GetComponent<BoardModel>().Level = level;

        QuestionGenerator = new GameObject();
        QuestionGenerator.AddComponent<ArithmeticQuestionGenerator>();
        QuestionGenerator.GetComponent<ArithmeticQuestionGenerator>().Level = level;
        UpdateVegetablesRemaining();
    }

    void RemoveCarrotFromBoard()
    {
        //Show find carrot, make visible and move to postion of tile
        Vegetable = CarrotsArray[CarrotsRemaining - 1];
        Vegetable.SetActive(true);
        Vegetable.transform.position = SelectedTilePos;

        //Update carrots remaining text
        
        CarrotsRemaining -= 1;
        UpdateVegetablesRemaining();
        if (CarrotsRemaining > 0)
        {
            Text text = SpeechText.GetComponent<Text>();
            text.text = SteveQuotes.CarrotFound;
        }
    }

    void RemoveBroccoliFromBoard()
    {
        //Show found broccoli, make visible and move to postion of tile
        Vegetable = BroccoliArray[BroccoliRemaining - 1];
        Vegetable.SetActive(true);
        Vegetable.transform.position = SelectedTilePos;

        //Update broccoli remaining text
        BroccoliRemaining -= 1;
        UpdateVegetablesRemaining();

        if (CarrotsRemaining > 0)
        {
            Text text = SpeechText.GetComponent<Text>();
            text.text = SteveQuotes.BroccoliFound;
        }
    }

    void PushVegetablesBack()
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
    IEnumerator ShowLevelComplete()
    {
        StaticVariables.GameStatus = "LevelComplete";
        Text text = SpeechText.GetComponent<Text>();
        text.text = SteveQuotes.Free;
        StartCoroutine(FadeSpeechBubble());
        //Wait 1 second
        yield return new WaitForSeconds(1);
        LevelComplete.SetActive(true);
    }

    void ShowLevelFailed()
    {
        StaticVariables.GameStatus = "LevelFailed";                   
        Debug.Log ("Level Failed");
        StaticVariables.Score = startingScore;
        LevelFailed.SetActive(true);
    }
}
