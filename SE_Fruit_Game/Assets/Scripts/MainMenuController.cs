using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGameButtonClicked()
    {
        SceneManager.LoadScene("MathsLevel_1");
        StaticVariables.Score = 0;
        StaticVariables.Level = 1;
    }

    public void LoadGameButtonClicked()
    {
        LoadGame();
        if (StaticVariables.Level == 1)
        {
            SceneManager.LoadScene("MathsLevel_1");
        }
        else if (StaticVariables.Level == 2)
        {
            SceneManager.LoadScene("MathsLevel_2");
        }
        else if (StaticVariables.Level == 3)
        {
            SceneManager.LoadScene("MathsLevel_3");
        }
        Debug.Log("Load Game button clicked");
        
    }

    public void QuitToDesktopButtonClicked ()
    {
        Application.Quit();
    }

    void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            StaticVariables.Score = PlayerPrefs.GetInt("SavedScore");
            StaticVariables.Level = PlayerPrefs.GetInt("SavedLevel");
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no saved data!");
        }
    }
}
