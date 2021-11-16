using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGameButtonClicked()
    {
        SceneManager.LoadScene("MathsLevel_1");
    }

    public void LoadGameButtonClicked()
    {
        Debug.Log("Load Game button clicked");
    }

    public void QuitToDesktopButtonClicked ()
    {
        Application.Quit();
    }
}
