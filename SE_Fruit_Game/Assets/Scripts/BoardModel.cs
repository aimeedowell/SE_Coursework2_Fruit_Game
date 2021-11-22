using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour{
    private string[,] gridBoard = new string[3, 3]; 
    private int level;

    public BoardModel(int level){
        this.level = level; 
        initialiseBoard();
    }

    public int Level {
        get {return level;}
        set {
            level = value;
        }
    }

    private void initialiseBoard(){
        this.gridBoard[1,1] = "Strawberry";

        for (int carrot = 0; carrot < 2; carrot++){
            addCarrot();
        }

    }

    public void addCarrot(){
        bool foundCell = false; 

        System.Random rnd = new System.Random();

        while (!foundCell) {
            int firstNum = rnd.Next(3);
            int secondNum = rnd.Next(3);

            if (this.gridBoard[firstNum, secondNum] == null && getDistance(firstNum, secondNum) > 1) {
                this.gridBoard[firstNum, secondNum] = "Carrot";
                foundCell = true;
            } 
        }

    }

    public double getDistance(int xCoord, int yCoord){
        double dist = System.Math.Sqrt(System.Math.Pow(xCoord - 1, 2)+ System.Math.Pow(yCoord - 1, 2));
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


