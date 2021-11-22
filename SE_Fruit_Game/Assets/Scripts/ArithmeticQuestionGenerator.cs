using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ArithmeticQuestionGenerator : MonoBehaviour {
    private int level; 
    private int upperRange;

    private string[] operations = new string[] {"+", "-", "*", "/"};
    public ArithmeticQuestionGenerator(int level) {
        this.level = level;
        setUpperRange(); 
    }

    private void setUpperRange() {
        int upperBoundary = 5;

        if (this.level == 2 || this.level == 3) {
            upperBoundary = 10;
        } else if (this.level == 4){
            upperBoundary = 20;
        } else if (this.level == 5) {
            upperBoundary = 50;
        }

        this.upperRange = upperBoundary;

    }

    public Tuple<string, double> generateQuestion(){
        bool evenFlag = false; 
        int firstNum, secondNum;
        
        do {
            firstNum = getRandomNum(1,this.upperRange);
            secondNum = getRandomNum(1,this.upperRange);

            if (this.level <= 2 ){
                evenFlag = isEven(firstNum) && isEven(secondNum) ? false : true;
            }

        } while (evenFlag);

        string equationOperator = "+";

        equationOperator = operations[getRandomNum(0,4)];

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
        System.Random rnd = new System.Random(); 

        int rndNum = rnd.Next(lowerBound, upperBound);
        return rndNum; 
    }


    private bool isEven(int num) {
        return num % 2 == 0; 
    }
}
