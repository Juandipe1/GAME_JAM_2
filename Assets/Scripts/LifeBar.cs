using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LifeBar : MonoBehaviour
{
    [Header("Avatares disponibles")]
    public List<GameObject> avataresJugador;
    public List<GameObject> avataresCPU;

    [Header("Puntos de aparición")]
    public Transform puntoJugador;
    public Transform puntoCPU;

    [Header("Referencias UI")]
    public Image playerLifeBar;
    public Image cpuLifeBar;
    public TMP_Text rondaTexto;
    public TMP_Text resultadoTexto;

    [Header("Tiempos entre pasos")]
    public float delayEntrePasos = 1f;

    [SerializeField] Animator robotCombatAnimation;
    [SerializeField] Animator humanCombatAnimation;

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

        // Elegir e instanciar avatar aleatorio para el jugador
        int indiceJugador = Random.Range(0, avataresJugador.Count);
        GameObject avatarJugador = Instantiate(avataresJugador[indiceJugador], puntoJugador.position, puntoJugador.rotation);

        // Elegir e instanciar avatar aleatorio para la CPU
        int indiceCPU = Random.Range(0, avataresCPU.Count);
        GameObject avatarCPU = Instantiate(avataresCPU[indiceCPU], puntoCPU.position, puntoCPU.rotation);

        // Obtener animators desde los nuevos avatares instanciados
        humanCombatAnimation = avatarJugador.GetComponent<Animator>();
        robotCombatAnimation = avatarCPU.GetComponent<Animator>();

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
                Debug.Log("Empate — nadie pierde vida.");
                robotCombatAnimation.SetTrigger(cpuChoice);
                humanCombatAnimation.SetTrigger(playerChoice);
                resultado = "Empate";
            }
            else if (cpuChoice == "Attack" && playerChoice == "Defense")
            {
                robotCombatAnimation.SetTrigger("Attack");
                humanCombatAnimation.SetTrigger("Defense");
                Debug.Log("Ganaste esta ronda. CPU pierde una vida.");
                resultado = "Ganaste";
                vidasCPU--;

            }
            else if (cpuChoice == "Defense" && playerChoice == "Grab")
            {
                robotCombatAnimation.SetTrigger("Defense");
                humanCombatAnimation.SetTrigger("Grab");
                Debug.Log("Ganaste esta ronda. CPU pierde una vida.");
                resultado = "Ganaste";
                vidasCPU--;

            }
            else if (cpuChoice == "Grab" && playerChoice == "Attack")
            {
                robotCombatAnimation.SetTrigger("Grab");
                humanCombatAnimation.SetTrigger("Attack");
                Debug.Log("Ganaste esta ronda. CPU pierde una vida.");
                resultado = "Ganaste";
                vidasCPU--;

            }
            else if (playerChoice == "Attack" && cpuChoice == "Defense")
            {
                robotCombatAnimation.SetTrigger("Defense");
                humanCombatAnimation.SetTrigger("Attack");
                Debug.Log("Perdiste esta ronda. Tú pierdes una vida.");
                resultado = "Perdiste";
                vidasJugador--;
            }
            else if (playerChoice == "Defense" && cpuChoice == "Grab")
            {
                robotCombatAnimation.SetTrigger("Grab");
                humanCombatAnimation.SetTrigger("Defense");
                Debug.Log("Perdiste esta ronda. Tú pierdes una vida.");
                resultado = "Perdiste";
                vidasJugador--;
            }
            else if (playerChoice == "Grab" && cpuChoice == "Attack")
            {
                robotCombatAnimation.SetTrigger("Attack");
                humanCombatAnimation.SetTrigger("Grab");
                Debug.Log("Perdiste esta ronda. Tú pierdes una vida.");
                resultado = "Perdiste";
                vidasJugador--;
            }
            else 
            {
                resultado = "...";
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
