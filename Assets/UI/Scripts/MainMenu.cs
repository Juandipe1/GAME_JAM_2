using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RoundSetupManager roundSetupManager;
    public void PlayGame()
    {
        roundSetupManager.MostrarPanelConfiguracion();
        //SceneManager.LoadScene("TestGameplay");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

}
