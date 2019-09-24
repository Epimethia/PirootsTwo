using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitButtonPressed()
    {
        Debug.Log("Application Quit");
        int PreviousHighScore = PlayerPrefs.GetInt("PreviousHighScore");
        int CurrentScore = PlayerPrefs.GetInt("CurrentScore");

        if(CurrentScore > PreviousHighScore)
        {
            PlayerPrefs.SetInt("PreviousHighScore", CurrentScore);
            PlayerPrefs.SetInt("CurrentScore", 0);
        }
        Application.Quit();
    }
}
