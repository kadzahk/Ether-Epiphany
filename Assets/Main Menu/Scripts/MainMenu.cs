using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // StartGame is called when the button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // QuitGame is called when the button is pressed
    public void QuitGame()
    {
        Application.Quit();
    }
}
