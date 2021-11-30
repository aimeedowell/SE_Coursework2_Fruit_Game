using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BoardModel : MonoBehaviour
{
    private string[,] gridBoard; 
    private int level;

   public void Start() {
        initialiseBoard();
    }

    public int Level {
        get {return level;}
        set {
            level = value;
        }
    }

    public string[,] GridBoard {
        get {return gridBoard;}
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



    // Movement of Vegetables
    public int moveVegetables() {
        int gridSize = getGridSize();
        List <string> RepositionedCarrots = new List<string>();
        

        // gridBoard = gridBoard;        // This is the grid where carrots are updated
        //int[,] positionToIgnore;
        for (int i = 0; i < gridSize; i = i + 1) {
        
            for(int j = 0; j < gridSize; j = j + 1){

                string CarrotCheck = i.ToString() + j.ToString();
                
                if(this.gridBoard[i,j] == "Carrot" && !RepositionedCarrots.Contains(CarrotCheck)){
                    
                    var newPos  = GetNewCarrotPosition(gridSize,i,j);
                    RepositionedCarrots.Add(newPos);         
                }
                
            }

        }
        return 0;
    }


    public string GetNewCarrotPosition(int gridSize, int xcor, int ycor){

        Boolean CarrotPositioned = false;
        int CarrotMovement = 1;
        string ReturnString = ""; 

        
        while(!CarrotPositioned){ 
            
            
            double up =    getDistance(xcor, ycor) - getDistance(xcor - CarrotMovement, ycor);
            double down =  getDistance(xcor, ycor) - getDistance(xcor + CarrotMovement, ycor);
            double left =  getDistance(xcor, ycor) - getDistance(xcor , ycor - CarrotMovement);
            double right = getDistance(xcor, ycor) - getDistance(xcor , ycor + CarrotMovement);
            
            List<Tuple<double, string, int>> Movements = new List<Tuple<double, string, int>>();

            System.Random rnd = new System.Random();

            if(up >= 0 && this.gridBoard[xcor - CarrotMovement, ycor] == null){
                Movements.Add(new Tuple<double, string, int>(up, "up",rnd.Next(100)));   
            }
            else if(up >= 0 && (this.gridBoard[xcor - CarrotMovement, ycor] != "Found" || 
                           this.gridBoard[xcor - CarrotMovement, ycor] != "Carrot"|| 
                           this.gridBoard[xcor - CarrotMovement, ycor] != "Guessed")) {
                Movements.Add(new Tuple<double, string, int>(up, "up",rnd.Next(100)));
            }


            if(down >= 0 && this.gridBoard[xcor + CarrotMovement, ycor] == null){
                Movements.Add(new Tuple<double, string, int>(down, "down",rnd.Next(100)));
            }
            else if(down >= 0 && (this.gridBoard[xcor + CarrotMovement, ycor] != "Found" || 
                           this.gridBoard[xcor + CarrotMovement, ycor] != "Carrot"|| 
                           this.gridBoard[xcor + CarrotMovement, ycor] !="Guessed")) {   
            Movements.Add(new Tuple<double, string, int>(down, "down",rnd.Next(100)));}


            if(left >= 0 && this.gridBoard[xcor , ycor - CarrotMovement] == null){
                Movements.Add(new Tuple<double, string, int>(left, "left",rnd.Next(100)));
            }            
            else if(left >= 0 && (this.gridBoard[xcor , ycor - CarrotMovement] != "Found" || 
                           this.gridBoard[xcor , ycor - CarrotMovement] != "Carrot"|| 
                           this.gridBoard[xcor , ycor - CarrotMovement] !="Guessed")) {  
                Movements.Add(new Tuple<double, string, int>(left, "left",rnd.Next(100)));}


            if(right >= 0 && this.gridBoard[xcor , ycor + CarrotMovement] == null){
                Movements.Add(new Tuple<double, string, int>(right, "right",rnd.Next(100)));
            }            
            else if(right >= 0 && (this.gridBoard[xcor , ycor + CarrotMovement] != "Found" || 
                           this.gridBoard[xcor , ycor + CarrotMovement] != "Carrot"|| 
                           this.gridBoard[xcor , ycor + CarrotMovement] != "Guessed")) {
                Movements.Add(new Tuple<double, string, int>(right, "right",rnd.Next(100)));}



            if (Movements.Count == 0){
                CarrotMovement = CarrotMovement + 1;
                int[,] PositionIgnore = {{-1,-1}};
            }
            else {
                Movements.Sort((x, y) => {
                    int result = y.Item1.CompareTo(x.Item1);
                    return result == 0 ? y.Item3.CompareTo(x.Item3) : result;
                });

                var firstElement = Movements.First();

                if(firstElement.Item2 == "up"){
                this.gridBoard[xcor - CarrotMovement, ycor] = "Carrot";
                ReturnString = (xcor - CarrotMovement).ToString() + ycor.ToString();

                
                }

                if(firstElement.Item2 == "down"){
                this.gridBoard[xcor + CarrotMovement, ycor] = "Carrot";
                ReturnString = (xcor + CarrotMovement).ToString() + ycor.ToString();
                
                }

                if(firstElement.Item2 == "left"){
                this.gridBoard[xcor , ycor - CarrotMovement] = "Carrot";
                ReturnString = (xcor).ToString() + (ycor - CarrotMovement).ToString();
                
                }

                if(firstElement.Item2 == "right"){
                this.gridBoard[xcor , ycor + CarrotMovement] = "Carrot";
                ReturnString = (xcor).ToString() + (ycor + CarrotMovement).ToString();
               
                }


                this.gridBoard[xcor, ycor] = "null";
                CarrotPositioned = true;
                
            }



        }
        return ReturnString;



    }
}


