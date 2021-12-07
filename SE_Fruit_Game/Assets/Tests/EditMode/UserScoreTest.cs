using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UserScoreTest
{
    // Test to check if score is returned 
    [Test]
    public void IncrementScoreTest()
    {
        UserScore scorer = new UserScore(); 
        scorer.Start(); 
        scorer.incrementScore();

        Assert.AreEqual(100, StaticVariables.Score);
    }

    [Test]
    public void HalveScoreTest()
    {
        UserScore scorer = new UserScore(); 
        scorer.Start(); 

        scorer.halveScore(); 
        scorer.incrementScore();

        Assert.AreEqual(50, StaticVariables.Score);
    }

    [Test]
    public void HalveScoreRoundingTest()
    {
        UserScore scorer = new UserScore(); 
        scorer.Start(); 

        scorer.halveScore();
        scorer.halveScore();
        scorer.halveScore();
        scorer.halveScore();
        scorer.halveScore(); 
        scorer.halveScore(); 
        scorer.incrementScore();

        Assert.AreEqual(2, StaticVariables.Score);
    }

    [Test]
    public void NewQuestionScoreTest()
    {
        UserScore scorer = new UserScore(); 
        scorer.Start(); 

        scorer.halveScore(); 
        scorer.incrementScore();
        scorer.newQuestion();
        scorer.incrementScore();

        Assert.AreEqual(150, StaticVariables.Score);
    }

}
