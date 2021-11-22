using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneController : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
