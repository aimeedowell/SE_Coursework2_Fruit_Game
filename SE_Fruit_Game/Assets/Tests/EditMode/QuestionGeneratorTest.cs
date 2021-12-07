using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Data;
public class QuestionGeneratorTest
{
    // A test to check if the Arithmetic question generator works as predicted 
    [Test]
    public void QuestionGeneratorTestCorrectAnswer()
    {
        // Use the Assert class to test conditions
        ArithmeticQuestionGenerator qBank = new ArithmeticQuestionGenerator();
        qBank.Level = 2; 

        Tuple<string, double> questionAnswer = qBank.generateQuestion();
        Debug.Log(String.Format("{0} {1}", questionAnswer.Item1, questionAnswer.Item2));

        double result = System.Math.Round(Convert.ToDouble(new DataTable().Compute(questionAnswer.Item1, null)));

        Assert.AreEqual(questionAnswer.Item2, result);
    }

}
