using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMathsLevelFromMainMenu : MonoBehaviour
{
    public void StartLevelButtonClicked(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
