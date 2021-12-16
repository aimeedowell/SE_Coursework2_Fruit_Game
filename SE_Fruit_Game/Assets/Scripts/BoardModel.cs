using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BoardModel : MonoBehaviour
{
    private string[,] gridBoard; 
    List<int> currentVegPositions = new List<int>();
    List<int> previousVegPositions = new List<int>();
    private int level;
    private bool levelFailed = false;

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
        gridBoard = new string[getGridSizeX(), getGridSizeY()]; 
        levelFailed = false;
        
        if (getGridSizeX() == 3) {
            this.gridBoard[1,1] = "Strawberry";
        } 
        else if(getGridSizeX() == 5) {
            this.gridBoard[2,2] = "Strawberry";
        }else {
            this.gridBoard[3,1] = "Strawberry";
        }
        
        if (level > 1)
        {
            for (int broc = 0; broc < 1; broc++){
                addBroccoli();
            }
            for (int carrot = 0; carrot < 2; carrot++){
                addCarrot();
            }
            for (int banana = 0; banana < 2; banana++){
                addBanana();
            }
        }
        else{
            for (int carrot = 0; carrot < 2; carrot++){
                addCarrot();
            }
        }
    }

    public int getGridSizeX()
    {
        int gridSize = 0;
        
        if (this.level == 1) {
            gridSize = 3;
        } 
        else if (this.level == 2) {
            gridSize = 5;
        }
        else {
            gridSize = 7;
        }
        return gridSize;
    }

    public int getGridSizeY()
    {
        int gridSize = 0;
        
        if (this.level == 1 || this.level == 3) {
            gridSize = 3;
        } 
        else {
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
            int firstNum = rnd.Next(getGridSizeX());
            int secondNum = rnd.Next(getGridSizeY());

            if (this.gridBoard[firstNum, secondNum] == null && getDistance(firstNum, secondNum) > minReqDistance) {
                this.gridBoard[firstNum, secondNum] = "Carrot";
                Debug.Log("Carrot: x is " + firstNum + " and y is" + secondNum);  
                previousVegPositions.Add(firstNum);
                previousVegPositions.Add(secondNum);
                foundCell = true;
            } 
        }

    }

    public void addBanana(){
        bool foundCell = false; 

        System.Random rnd = new System.Random();

        while (!foundCell) {
            int firstNum = rnd.Next(getGridSizeX());
            int secondNum = rnd.Next(getGridSizeY());

            if (this.gridBoard[firstNum, secondNum] == null && this.gridBoard[firstNum, secondNum] != "Carrot" 
            && this.gridBoard[firstNum, secondNum] != "Broccoli"
            && ((firstNum - secondNum)%2) != 0)
            {
                this.gridBoard[firstNum, secondNum] = "Banana";
                Debug.Log("Banana: x is " + firstNum + " and y is" + secondNum);       
                foundCell = true;
            } 
        }
    }
        public void addBroccoli(){
        int minReqDistance = 2;
        bool foundCell = false; 

        System.Random rnd = new System.Random();

        while (!foundCell) {
            int firstNum = rnd.Next(getGridSizeX());
            int secondNum = rnd.Next(getGridSizeY());

            if (this.gridBoard[firstNum, secondNum] == null && this.gridBoard[firstNum, secondNum] != "Carrot" && getDistance(firstNum, secondNum) > minReqDistance
                && ((firstNum - secondNum)%2) == 0) {
                this.gridBoard[firstNum, secondNum] = "Broccoli";
                previousVegPositions.Add(firstNum);
                previousVegPositions.Add(secondNum);
                Debug.Log("Broccoli: x is " + firstNum + " and y is" + secondNum);  
                foundCell = true;
            } 
        }
    }

    public double getDistance(int xCoord, int yCoord){
        int steveLocationX = 1; 
        int steveLocationY = 1; 
        if (Level == 2) {
            steveLocationX = 2;
            steveLocationY = 2;
        }
        if (Level == 3) {
            steveLocationX = 3;
            steveLocationY = 1;
        }
        double dist = System.Math.Sqrt(System.Math.Pow(xCoord - steveLocationX, 2)+ System.Math.Pow(yCoord - steveLocationY, 2));
        return dist;
    }

    private bool isCarrotPresent(int xCoord, int yCoord) {
        bool carrotPresent = false;
        if (this.gridBoard[xCoord, yCoord] == "Carrot"){
            carrotPresent = true;
        }

        return carrotPresent;
    }

      private bool isBananaPresent(int xCoord, int yCoord) 
    {
        bool bananaPresent = false;
        if (this.gridBoard[xCoord, yCoord] == "Banana")
            bananaPresent = true;
    
        return bananaPresent;
    }

    private bool isBroccoliPresent(int xCoord, int yCoord) 
    {
        bool brocPresent = false;
        if (this.gridBoard[xCoord, yCoord] == "Broccoli")
            brocPresent = true;
    
        return brocPresent;
    }


    public string makeGuess(int xCoord, int yCoord) {
        string cellFlag;
 
        if (isCarrotPresent(xCoord, yCoord)){
            cellFlag = "Carrot";
        }
        else if (isBananaPresent(xCoord, yCoord)){
            cellFlag = "Banana";
        }
        else if (isBroccoliPresent(xCoord, yCoord)){
            cellFlag = "Broccoli";
        }
        else {
            cellFlag = "null";
        }
 
        return cellFlag;
    }

    // Movement of Vegetables
    public int moveVegetables() {
        int gridSizeX = getGridSizeX();
        int gridSizeY = getGridSizeY();
        List <string> RepositionedVegetables = new List<string>();
        previousVegPositions.Clear();
        

        for (int i = 0; i < gridSizeX; i = i + 1) {     
        
            for(int j = 0; j < gridSizeY; j = j + 1){

                string VegetableCheck = i.ToString() + j.ToString();            // To not change the position of repositioned vegetables
                
                if(this.gridBoard[i,j] == "Carrot" && !RepositionedVegetables.Contains(VegetableCheck)){
                    
                    previousVegPositions.Add(i);
                    previousVegPositions.Add(j);
                    var newPos  = GetNewCarrotPosition(i, j);
                    RepositionedVegetables.Add(newPos);         
                    GetVegetablePosition();
                    Debug.Log("Carrot: x is " + i + " and y is" + j);
                }
                else if(this.gridBoard[i,j] == "Broccoli" && !RepositionedVegetables.Contains(VegetableCheck)){
                    
                    previousVegPositions.Add(i);
                    previousVegPositions.Add(j);
                    var newPos  = GetNewBroccoliPosition(i, j);
                    RepositionedVegetables.Add(newPos); 
                    GetVegetablePosition();
                    Debug.Log("Broccoli: x is " + i + " and y is" + j);      
                }
            }
        }
        return 0;
    }


    public string GetNewCarrotPosition(int xcor, int ycor){

        Boolean CarrotPositioned = false;
        int CarrotMovement = 1;
        string ReturnString = ""; 

        
        while(!CarrotPositioned){ 
            
            // To get the distance of carrots after movement
            double up =    getDistance(xcor, ycor) - getDistance(xcor - CarrotMovement, ycor);
            double down =  getDistance(xcor, ycor) - getDistance(xcor + CarrotMovement, ycor);
            double left =  getDistance(xcor, ycor) - getDistance(xcor , ycor - CarrotMovement);
            double right = getDistance(xcor, ycor) - getDistance(xcor , ycor + CarrotMovement);
            
            List<Tuple<double, string, int>> Movements = new List<Tuple<double, string, int>>();

            System.Random rnd = new System.Random();

            // Selecting only valid movements
            if(up >= 0 && (this.gridBoard[xcor - CarrotMovement, ycor] == null || this.gridBoard[xcor - CarrotMovement, ycor] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(up, "up",rnd.Next(100)));   
            }


            if(down >= 0 && (this.gridBoard[xcor + CarrotMovement, ycor] == null || this.gridBoard[xcor + CarrotMovement, ycor] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(down, "down",rnd.Next(100)));
            }

            if(left >= 0 && (this.gridBoard[xcor , ycor - CarrotMovement] == null || this.gridBoard[xcor , ycor - CarrotMovement] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(left, "left",rnd.Next(100)));
            }        
            
            if(right >= 0 && (this.gridBoard[xcor , ycor + CarrotMovement] == null || this.gridBoard[xcor , ycor + CarrotMovement] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(right, "right",rnd.Next(100)));
            }    

            // Set the max jump limit of vegetable according to the level
            if (Movements.Count == 0 && CarrotMovement > this.level){
                CarrotPositioned = true;
            }
            else if (Movements.Count == 0){
                CarrotMovement = CarrotMovement + 1;            // increasing carrot jump limit if there are no legal moves left
                int[,] PositionIgnore = {{-1,-1}};
            }
            else {
                Movements.Sort((x, y) => {
                    int result = y.Item1.CompareTo(x.Item1);
                    return result == 0 ? y.Item3.CompareTo(x.Item3) : result;               // sorting the list according to best move - i.e. the max change in distance between veg and fruit
                });

                var firstElement = Movements.First();

                // Selecting the best movement

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

    public string GetNewBroccoliPosition(int xcor, int ycor){

        Boolean BroccoliPositioned = false;
        int BroccoliMovement = 1;
        string ReturnString = ""; 

        
        while(!BroccoliPositioned){ 
            
            // To get the distance of brocolli after movement
            
            double up_left    = getDistance(xcor, ycor) - getDistance(xcor - BroccoliMovement, ycor - BroccoliMovement);
            double up_right   = getDistance(xcor, ycor) - getDistance(xcor + BroccoliMovement, ycor - BroccoliMovement);
            double down_left  = getDistance(xcor, ycor) - getDistance(xcor - BroccoliMovement, ycor + BroccoliMovement);
            double down_right = getDistance(xcor, ycor) - getDistance(xcor + BroccoliMovement, ycor + BroccoliMovement);
            
            List<Tuple<double, string, int>> Movements = new List<Tuple<double, string, int>>();

            System.Random rnd = new System.Random();

            // To select only valid movement of broccoli
            if(up_left >= 0 && (this.gridBoard[xcor - BroccoliMovement, ycor - BroccoliMovement] == null || this.gridBoard[xcor - BroccoliMovement, ycor - BroccoliMovement] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(up_left, "up_left",rnd.Next(100)));   
            }


            if(up_right >= 0 && (this.gridBoard[xcor + BroccoliMovement, ycor - BroccoliMovement] == null || this.gridBoard[xcor + BroccoliMovement, ycor - BroccoliMovement] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(up_right, "up_right",rnd.Next(100)));
            }

            if(down_left >= 0 && (this.gridBoard[xcor - BroccoliMovement, ycor + BroccoliMovement] == null || this.gridBoard[xcor - BroccoliMovement, ycor + BroccoliMovement] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(down_left, "down_left",rnd.Next(100)));
            }        
            
            if(down_right >= 0 && (this.gridBoard[xcor + BroccoliMovement, ycor + BroccoliMovement] == null || this.gridBoard[xcor + BroccoliMovement, ycor + BroccoliMovement] == "Strawberry")){
                Movements.Add(new Tuple<double, string, int>(down_right, "down_right",rnd.Next(100)));
            }    


            // Set the max jump limit of vegetable according to the level

            if (Movements.Count == 0 && BroccoliMovement >= this.level){
                BroccoliMovement = BroccoliMovement + 1;
                int[,] PositionIgnore = {{-1,-1}};
                BroccoliPositioned = true;
            }
            else if (Movements.Count == 0){
                BroccoliMovement = BroccoliMovement + 1;
                int[,] PositionIgnore = {{-1,-1}};
            }            
            else {
                Movements.Sort((x, y) => {
                    int result = y.Item1.CompareTo(x.Item1);
                    return result == 0 ? y.Item3.CompareTo(x.Item3) : result;     // sorting the list according to best move - i.e. the max change in distance between veg and fruit
                });

                var firstElement = Movements.First();



                // Selecting the best movement
                if(firstElement.Item2 == "up_left"){
                this.gridBoard[xcor - BroccoliMovement, ycor - BroccoliMovement] = "Broccoli";
                ReturnString = (xcor - BroccoliMovement).ToString() + (ycor - BroccoliMovement).ToString(); 

                
                }

                if(firstElement.Item2 == "up_right"){
                this.gridBoard[xcor + BroccoliMovement, ycor - BroccoliMovement] = "Broccoli";
                ReturnString = (xcor + BroccoliMovement).ToString() + (ycor - BroccoliMovement).ToString();
                
                }

                if(firstElement.Item2 == "down_left"){
                this.gridBoard[xcor - BroccoliMovement, ycor + BroccoliMovement] = "Broccoli";
                ReturnString = (xcor - BroccoliMovement).ToString() + (ycor + BroccoliMovement).ToString();
                
                }

                if(firstElement.Item2 == "down_right"){
                this.gridBoard[xcor + BroccoliMovement, ycor + BroccoliMovement] = "Broccoli";
                ReturnString = (xcor + BroccoliMovement).ToString() + (ycor + BroccoliMovement).ToString();
               
                }
                this.gridBoard[xcor, ycor] = "null";
                BroccoliPositioned = true;
                
            }
        }
        return ReturnString;
    }


    public List<int> GetVegetablePosition()
    {
        currentVegPositions.Clear();
        for (int i = 0; i < getGridSizeX(); i = i + 1) 
        {
            for (int j = 0; j < getGridSizeY(); j = j + 1)
            {
                if (this.gridBoard[i,j] == "Carrot" || this.gridBoard[i,j] == "Broccoli")
                {
                    if (level == 1)
                    {
                        if (i == 1 && j == 1) //3X3 board 
                            levelFailed = true;
                    }
                    else if (level == 2)
                    {
                        if (i == 2 && j == 2) // 5X5 board 
                            levelFailed = true;
                    }
                    else 
                    {
                        if (i == 3 && j == 1) // 7X3 board 
                            levelFailed = true;
                    }
                    currentVegPositions.Add(i);
                    currentVegPositions.Add(j);
                }
            }
        }
        return currentVegPositions;
    }

    private List<int> GetPreviousVegPosition()
    {
        return previousVegPositions;
    }

    public void PushVegetablesBackOneMove()
    {
        for (int i = 0; i < currentVegPositions.Count(); i+=2)
        {
            int firstNum = GetVegetablePosition()[i];
            int secondNum = GetVegetablePosition()[i+1];
            int newFirstNum = GetPreviousVegPosition()[i];
            int newSecondNum = GetPreviousVegPosition()[i+1];
            if (this.gridBoard[firstNum,secondNum] == "Carrot")
            {
                this.gridBoard[firstNum,secondNum] = "null";
                this.gridBoard[newFirstNum, newSecondNum] = "Carrot";
            }
            else if (this.gridBoard[firstNum,secondNum] == "Broccoli")
            {
                this.gridBoard[firstNum,secondNum] = "null";
                this.gridBoard[newFirstNum, newSecondNum] = "Broccoli";
            }
        }
    }

    public bool HasLevelFailed()
    {
        return levelFailed;
    }
}


