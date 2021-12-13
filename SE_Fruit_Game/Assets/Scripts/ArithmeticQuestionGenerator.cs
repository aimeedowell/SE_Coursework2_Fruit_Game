using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ArithmeticQuestionGenerator : MonoBehaviour 
{
    private int level; 
    private int upperRange;
    private string[] operationsLevel1 = new string[] {"+", "-", "*", "/"};
    private string[] operationsLevel2 = new string[] {"+", "-", "*"};
      private string[] operationsLevel3 = new string[] {"+", "-"};

    private void Start() {
        
    }

    public int Level {
        get {return level;}
        set {
            level = value;
            setUpperRange(); 
        }
    }

    private void setUpperRange() {
        int upperBoundary = 10;

        if (this.level < 3) {
            upperBoundary = 10;
        } else 
            upperBoundary = 50;

        this.upperRange = upperBoundary;

    }

    public Tuple<string, double> generateQuestion(){
        bool foundNumbers = false; 

        string[] operations;

        if (level == 1)
            operations = operationsLevel1;
        else if (level == 2)
            operations = operationsLevel2;
        else 
            operations = operationsLevel3;
        

        int firstNum, secondNum;
        
        do {

            firstNum = getRandomNum(1,this.upperRange);
            secondNum = getRandomNum(1,this.upperRange);
            

            if (this.level == 1 ){
                foundNumbers = isEven(firstNum) && isEven(secondNum) ? true : false;
            }
            else if (this.level == 2 ){
                foundNumbers = isOdd(firstNum) && isOdd(secondNum) ? true : false;
            }
            else 
                foundNumbers = true;

        } while (!foundNumbers);

        string equationOperator = "+";

        equationOperator = operations[getRandomNum(0,operations.Length)];

        double correctAns = 0; 

        if (equationOperator == "+") {correctAns = firstNum + secondNum;}
        else if  (equationOperator == "*") {correctAns = firstNum * secondNum;}
        else if (equationOperator == "-") {correctAns = firstNum - secondNum;}
        else if (equationOperator == "/") {
            correctAns = (firstNum * 1.0)  / (secondNum * 1.0);
            
            if (!isEven(secondNum)) {
                correctAns = System.Math.Round(correctAns, 2);
            }
        }

        string equationString = firstNum.ToString() + " " + equationOperator.ToString() + " " + secondNum.ToString();

        return Tuple.Create(equationString, correctAns); 
    }

    private int getRandomNum(int lowerBound, int upperBound){
        int rndNum = UnityEngine.Random.Range(lowerBound, upperBound);
        return rndNum; 
    }


    private bool isEven(int num) {
        return num % 2 == 0; 
    }
    private bool isOdd(int num) {
        return num % 2 != 0; 
    }
}
