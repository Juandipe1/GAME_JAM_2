using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject panelPrincipal;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartButton;
    public GameObject canvasPlayer;
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        panelPrincipal.SetActive(false);

        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void TogglePause()
    {
        canvasPlayer.SetActive(false);
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        panelPrincipal.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    void ResumeGame()
    {
        canvasPlayer.SetActive(true);
        isPaused = false;
        pausePanel.SetActive(false);
        panelPrincipal.SetActive(false);
        Time.timeScale = 1f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté corriendo
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(0);
    }
}
