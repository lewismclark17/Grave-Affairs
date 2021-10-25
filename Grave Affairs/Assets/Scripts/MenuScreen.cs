using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public GameObject NewGameButton;
    public GameObject QuitButton;

    public void Begin()
    {
        SceneManager.LoadScene("Cutscene1");
    }

    public void Quit()
    {
        Application.Quit();     
    }
}
