using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController3x3 : MonoBehaviour
{

    //Global variable to hold current game status as string (TileSelection, TileSelected...)
    public string GameStatus = "TileSelection";

    //Global variable to hold currently selected tile object
    public GameObject SelectedTile;

    //Global variable to hold SelectedTile's simplified coordinates (determined in HighlightTile() by GetCoords())
    public int[] SelectedTileCoords = new int[2];

    public GameObject HighlightSquare;

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

    //Run at the start of the game level
    void Start()
    {
        //Make Highlight Square invisible
        HighlightSquare = GameObject.Find("HighlightSquare");
        HighlightSquare.SetActive(false);


    }

    //If GameStatus is TileSelection, on click highlight the selected tile and update GameStatus, SelectedTile and SelectedTileCoords
    public void SelectTile(GameObject obj)
    {
        //Check if GameStatus is TileSelection
        if (GameStatus == "TileSelection") {

            //Set global SelectedTile to the selected tile and global SelectedTileCoords to the coordinates of the selected tile
            SelectedTile = obj;
            SelectedTileCoords = GetCoords(obj);

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
