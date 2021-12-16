using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScore : MonoBehaviour
{
    private int score; 

    public void Start(){
        this.score = 100; 
    }

    public void incrementScore() {
        StaticVariables.Score += this.score; 
        this.score = 100; 
    }

    public void halveScore() {
        this.score = System.Convert.ToInt16(System.Math.Round(this.score/2.0, 2));
    }

    public void newQuestion() {
        this.score = 100; 
    }
}
