using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToStartCinematic()
    {
        SceneManager.LoadScene("StartCinematic");
    }

    public void GoToGame()
    {
        PlayerPrefs.SetInt("Speedrun", 0);
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToSpeedrun()
    {
        PlayerPrefs.SetInt("Speedrun", 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToEndGameCinematic()
    {
        SceneManager.LoadScene("EndCinematic");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
