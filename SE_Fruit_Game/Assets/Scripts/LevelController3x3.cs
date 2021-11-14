using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController3x3 : MonoBehaviour
{

    //Global variable to hold current game status as string (TileSelection, TileSelected...)
    public string GameStatus = "TileSelection";

    //Global variable to hold currently selected tile object
    public GameObject SelectedTile;

    public GameObject HighlightSquare;

    void Start()
    {
        //Make Highlight Square invisible
        HighlightSquare = GameObject.Find("HighlightSquare");
        HighlightSquare.SetActive(false);


    }

    //If GameStatus is TileSelection, on click highlight the selected tile and change GameStatus to TileSelected
    public void HighlightTile(GameObject obj)
    {
        //Check if GameStatus is TileSelection
        if (GameStatus == "TileSelection") {

            //Set global SelectedTile to the selected tile
            SelectedTile = obj;
            
            //Get position of selected tile and set to position of HighlightSquare
            Vector2 objPos = obj.GetComponent<RectTransform>().anchoredPosition;
            HighlightSquare.GetComponent<RectTransform>().anchoredPosition = objPos;

            //Make HighlightSquare visible
            HighlightSquare.SetActive(true);

            //Change GameStatus to "TileSelected"
            GameStatus = "TileSelected";
        }

    }
    
}
