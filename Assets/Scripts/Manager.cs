using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    [Header("Opciones")]
    [SerializeField] string[] choises;

    [Header("Delay entre rondas")]
    [SerializeField] float delayBetweenRounds = 1f;

    [Header("Referencias UI")]
    [SerializeField] TMP_Text playerLivesText;
    [SerializeField] TMP_Text cpuLivesText;
    [SerializeField] TMP_Text resultadoFinalText;

    List<string> playerChoises = new List<string>();
    List<string> cpuChoises = new List<string>();

    int playerLives;
    int cpuLives;


    public void IniciarJuegoDesdeRoundSetup()
    {
        int totalRounds = RoundSetupManager.eleccionesRondas.Count;
        playerLives = totalRounds;
        cpuLives = totalRounds;

        ActualizarTextoVidas();

        StartCoroutine(PlayAllRounds());
    }

    IEnumerator PlayAllRounds()
    {
        int totalRounds = RoundSetupManager.eleccionesRondas.Count;

        for (int i = 0; i < totalRounds; i++)
        {
            string playerChoiceUI = RoundSetupManager.eleccionesRondas[i];
            string playerChoice = ConvertChoiceToEnglish(playerChoiceUI);
            string cpuChoice = choises[Random.Range(0, choises.Length)];

            playerChoises.Add(playerChoice);
            cpuChoises.Add(cpuChoice);

            Debug.Log($"--- Ronda {i + 1} ---");
            Debug.Log($"Player: {playerChoice} | CPU: {cpuChoice}");

            //if (playerChoice == cpuChoice)
            //{
            //    Debug.Log("Empate — nadie pierde vida.");
            //    robotCombatAnimation.SetTrigger(cpuChoice);
            //    humanCombatAnimation.SetTrigger(playerChoice);
            //}
            //else if (cpuChoice == "Attack" && playerChoice == "Defense")
            //{
            //    robotCombatAnimation.SetTrigger("Attack");
            //    humanCombatAnimation.SetTrigger("Defense");
            //    Debug.Log("Ganaste esta ronda. CPU pierde una vida.");
            //    cpuLives--;
            //}
            //else if (cpuChoice == "Defense" && playerChoice == "Grab") 
            //{
            //    robotCombatAnimation.SetTrigger("Defense");
            //    humanCombatAnimation.SetTrigger("Grab");
            //    Debug.Log("Ganaste esta ronda. CPU pierde una vida.");
            //    cpuLives--;
            //}
            //else if (cpuChoice == "Grab" && playerChoice == "Attack")
            //{
            //    robotCombatAnimation.SetTrigger("Grab");
            //    humanCombatAnimation.SetTrigger("Attack");
            //    Debug.Log("Ganaste esta ronda. CPU pierde una vida.");
            //    cpuLives--;
            //}
            //else if (playerChoice == "Attack" && cpuChoice == "Defense")
            //{
            //    robotCombatAnimation.SetTrigger("Defense");
            //    humanCombatAnimation.SetTrigger("Attack");
            //    Debug.Log("Perdiste esta ronda. Tú pierdes una vida.");
            //    playerLives--;
            //}
            //else if (playerChoice == "Defense" && cpuChoice == "Grab")
            //{
            //    robotCombatAnimation.SetTrigger("Grab");
            //    humanCombatAnimation.SetTrigger("Defense");
            //    Debug.Log("Perdiste esta ronda. Tú pierdes una vida.");
            //    playerLives--;
            //}
            //else if (playerChoice == "Grab" && cpuChoice == "Attack")
            //{
            //    robotCombatAnimation.SetTrigger("Attack");
            //    humanCombatAnimation.SetTrigger("Grab");
            //    Debug.Log("Perdiste esta ronda. Tú pierdes una vida.");
            //    playerLives--;
            //}


            ActualizarTextoVidas();

            yield return new WaitForSeconds(delayBetweenRounds);
        }

        MostrarResultadoFinal();
    }

    void ActualizarTextoVidas()
    {
        playerLivesText.text = $"Jugador: {playerLives} vidas";
        cpuLivesText.text = $"CPU: {cpuLives} vidas";
    }

    void MostrarResultadoFinal()
    {
        string resultado;
        if (playerLives > cpuLives)
        {
            resultado = "¡Ganaste el juego!";
        }
        else if (cpuLives > playerLives)
        {
            resultado = "Perdiste el juego.";
        }
        else
        {
            resultado = "El juego terminó en empate.";
        }

        Debug.Log(resultado);
        if (resultadoFinalText != null)
            resultadoFinalText.text = resultado;
    }

    private string ConvertChoiceToEnglish(string uiChoice)
    {
        switch (uiChoice)
        {
            case "Ataque": return "Ataque";
            case "Agarre": return "Agarre";
            case "Defensa": return "Defensa";
            default: return "Ataque";
        }
    }
}
