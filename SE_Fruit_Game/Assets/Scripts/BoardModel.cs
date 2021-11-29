using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardModel : MonoBehaviour
{
    private string[,] gridBoard; 
    private int level;

   private void Start() {
        initialiseBoard();
    }

    public int Level {
        get {return level;}
        set {
            level = value;
        }
    }

    private void initialiseBoard()
    {
        gridBoard = new string[getGridSize(), getGridSize()]; 
        
        if (getGridSize() == 3) {
            this.gridBoard[1,1] = "Strawberry";
        } else {
            this.gridBoard[2,2] = "Strawberry";
        }
        

        for (int carrot = 0; carrot < 2; carrot++){
            addCarrot();
        }

    }

    public int getGridSize(){
        int gridSize = 0;
        
        if (this.level == 1) {
            gridSize = 3;
        } else {
            gridSize = 5;
        }

        return gridSize;
    }

    public void addCarrot(){
        int minReqDistance = 1;
        bool foundCell = false; 

        if (Level == 2){
            minReqDistance = 2;
        }

        System.Random rnd = new System.Random();

        while (!foundCell) {
            int firstNum = rnd.Next(getGridSize());
            int secondNum = rnd.Next(getGridSize());

            if (this.gridBoard[firstNum, secondNum] == null && getDistance(firstNum, secondNum) > minReqDistance) {
                this.gridBoard[firstNum, secondNum] = "Carrot";
                foundCell = true;
            } 
        }

    }

    public double getDistance(int xCoord, int yCoord){
        int steveLocation = 1; 
        if (Level == 2) {
            steveLocation = 2;
        }
        double dist = System.Math.Sqrt(System.Math.Pow(xCoord - steveLocation, 2)+ System.Math.Pow(yCoord - steveLocation, 2));
        return dist;
    }

    private bool isCarrotPresent(int xCoord, int yCoord) {
        bool carrotPresent = false;
        if (this.gridBoard[xCoord, yCoord] == "Carrot"){
            carrotPresent = true;
        }

        return carrotPresent;
    }

    public string makeGuess(int xCoord, int yCoord) {
        string cellFlag = "Invalid";
  
        if (this.gridBoard[xCoord, yCoord] == null || this.gridBoard[xCoord, yCoord] == "Carrot") {
            
            if (isCarrotPresent(xCoord, yCoord)){
                cellFlag = "Carrot";
                
            } else {
                cellFlag = "null";
            }
            

            if (cellFlag != "null") {
                this.gridBoard[xCoord, yCoord] = "Found";
            } else {
                this.gridBoard[xCoord, yCoord] = "Guessed";
            }
        }
        

        return cellFlag;
    }
}


