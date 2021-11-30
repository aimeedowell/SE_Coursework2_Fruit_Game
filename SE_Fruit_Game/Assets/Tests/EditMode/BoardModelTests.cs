using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void GetCorrectLevelTest()
    {
        BoardModel boardModel = new BoardModel();
        boardModel.Level = 1; 
        Assert.AreEqual(1, boardModel.Level);
    }

    [Test]
    public void getCorrectGridSizeTest() {
        BoardModel boardModel = new BoardModel();
        boardModel.Level = 2; 
        Assert.AreEqual(5, boardModel.getGridSize());
    }

    [Test]
    public void checkCarrotsAddedTest(){
        BoardModel boardModel = new BoardModel();
        boardModel.Level = 1;
        boardModel.Start(); 

        string[,] currentGrid = boardModel.GridBoard; 

        int carrotCount = 0; 
        for (int row = 0; row<3;row++){
            for (int col = 0; col<3;col++){
                if (currentGrid[row, col] == "Carrot"){
                    carrotCount += 1; 
                }
            }
        } 

        Assert.AreEqual(2, carrotCount); 
    }

    [Test]
    public void checkDistanceTest() {
        BoardModel boardModel = new BoardModel();
        boardModel.Level = 1;
        
        double actualDistance = boardModel.getDistance(0,0); 

        Assert.AreEqual(1.4142135623730951d, actualDistance); 
    }
}
