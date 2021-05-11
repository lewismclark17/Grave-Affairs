using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public GameObject Button;

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
