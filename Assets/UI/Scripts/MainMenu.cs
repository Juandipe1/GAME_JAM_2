using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RoundSetupManager roundSetupManager;
    public GameObject animacionesMenu;
    public void PlayGame()
    {
        roundSetupManager.MostrarPanelConfiguracion();
        //SceneManager.LoadScene("TestGameplay");
        animacionesMenu.gameObject.SetActive(false);
    }

    public void ShowStory()
    {
        SceneManager.LoadScene("IntroStory");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

}
