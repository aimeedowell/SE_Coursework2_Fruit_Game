using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScore : MonoBehaviour
{
    private double currentScore;
    private double score; 

    public UserScore(){
        this.score = 100; 
        this.currentScore = 0; 
    }

    public double CurrentScore {
        get {return currentScore;}
    }

    public void incrementScore() {
        this.currentScore += this.score; 
        this.score = 100; 
    }

    public void halveScore() {
        this.score = System.Math.Round(this.score/2.0, 2);
    }
}
