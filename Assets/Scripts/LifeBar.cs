using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LifeBar : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image playerLifeBar;
    public Image cpuLifeBar;
    public TMP_Text rondaTexto;
    public TMP_Text resultadoTexto;

    [Header("Tiempos entre pasos")]
    public float delayEntrePasos = 1f;

    int vidasJugador;
    int vidasCPU;
    int totalRondas;

    string[] choises = { "Attack", "Grab", "Defense" };

    void Start()
    {
        vidasJugador = RoundSetupManager.vidasJugador;
        vidasCPU = RoundSetupManager.vidasCPU;
        totalRondas = RoundSetupManager.eleccionesRondas.Count;
        resultadoTexto.text = ""; // Limpiar texto de resultados al comenzar

        StartCoroutine(JugarRondas());
    }

    IEnumerator JugarRondas()
    {
        for (int i = 0; i < totalRondas; i++)
        {
            string playerUIChoice = RoundSetupManager.eleccionesRondas[i];
            string playerChoice = ConvertChoiceToEnglish(playerUIChoice);
            string cpuChoice = choises[Random.Range(0, choises.Length)];

            RoundSetupManager.eleccionesJugador.Add(playerChoice);
            RoundSetupManager.eleccionesCPU.Add(cpuChoice);

            // Mostrar número de ronda
            rondaTexto.text = $"Rondas: {i + 1}/{totalRondas}";

            // Mostrar elección del jugador
            resultadoTexto.text = $"Jugador eligió: {playerChoice}";
            yield return new WaitForSeconds(delayEntrePasos);

            // Mostrar elección de la CPU
            resultadoTexto.text = $"CPU eligió: {cpuChoice}";
            yield return new WaitForSeconds(delayEntrePasos);

            // Determinar resultado
            string resultado;
            if (playerChoice == cpuChoice)
            {
                resultado = "Empate";
            }
            else if ((cpuChoice == "Attack" && playerChoice == "Defense") ||
                     (cpuChoice == "Defense" && playerChoice == "Grab") ||
                     (cpuChoice == "Grab" && playerChoice == "Attack"))
            {
                resultado = "Ganaste";
                vidasCPU--;
            }
            else
            {
                resultado = "Perdiste";
                vidasJugador--;
            }

            RoundSetupManager.resultadosRonda.Add(resultado);

            // Mostrar resultado
            resultadoTexto.text = $"Resultado: {resultado}";
            ActualizarBarras();

            yield return new WaitForSeconds(delayEntrePasos * 1.5f);
        }

        MostrarResultadoFinal();
    }

    void ActualizarBarras()
    {
        float fillJugador = (float)vidasJugador / totalRondas;
        float fillCPU = (float)vidasCPU / totalRondas;

        playerLifeBar.fillAmount = fillJugador;
        cpuLifeBar.fillAmount = fillCPU;
    }

    void MostrarResultadoFinal()
    {
        string final;
        if (vidasJugador > vidasCPU)
            final = "¡Ganaste el juego!";
        else if (vidasJugador < vidasCPU)
            final = "Perdiste el juego.";
        else
            final = "Empate.";

        resultadoTexto.text = final;

        // Espera unos segundos y vuelve al menú
        StartCoroutine(VolverAlMenu());
    }

    IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos para que el jugador vea el resultado
        SceneManager.LoadScene("MainMenu"); // Asegúrate de usar el nombre correcto de tu escena de menú
}


    string ConvertChoiceToEnglish(string uiChoice)
    {
        switch (uiChoice)
        {
            case "Ataque": return "Attack";
            case "Agarre": return "Grab";
            case "Defensa": return "Defense";
            default: return "Attack";
        }
    }
}
