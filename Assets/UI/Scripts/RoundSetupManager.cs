// RoundSetupManager.cs
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RoundSetupManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject setupPanel; // Panel que aparece al presionar "Play"
    public TMP_InputField numRondasInput; // Input donde el jugador escribe el n�mero de rondas
    public GameObject dropdownItem; // Prefab que contiene un Label + Dropdown
    public Transform dropdownContainer; // Contenedor vertical de todos los Dropdowns
    public Button confirmarButton;      //Confirma las rondas para generar el dropdown
    public Button iniciarJuegoButton; // Bot�n para iniciar el juego.
    public Image instructions;
    public Manager manager;

    // Lista p�blica accesible por otros scripts para consultar las selecciones
    public static List<string> eleccionesRondas = new List<string>();

    private List<TMP_Dropdown> rondasDropdowns = new List<TMP_Dropdown>();

    // Mostrar el panel de configuraci�n
    public void MostrarPanelConfiguracion()
    {
        setupPanel.SetActive(true);
        iniciarJuegoButton.gameObject.SetActive(false);
    }

    // Al confirmar el n�mero de rondas
    public void ConfirmarRondas()
    {
        // Limpia dropdowns anteriores si exist�an
        foreach (Transform hijo in dropdownContainer)
            Destroy(hijo.gameObject);

        rondasDropdowns.Clear();
        eleccionesRondas.Clear();

        int numRondas;
        if (!int.TryParse(numRondasInput.text, out numRondas) || numRondas <= 0)
        {
            Debug.LogWarning("Por favor ingresa un n�mero v�lido de rondas.");
            return;
        }
        confirmarButton.gameObject.SetActive(false); //Desactiva el bot�n de confirmar rondas

        // Crear un Dropdown por cada ronda
        for (int i = 0; i < numRondas; i++)
        {
            GameObject nuevoDropdown = Instantiate(dropdownItem, dropdownContainer);
            TMP_Text label = nuevoDropdown.transform.Find("Label").GetComponent<TMP_Text>();
            label.text = "Ronda " + (i + 1);

            TMP_Dropdown drop = nuevoDropdown.GetComponentInChildren<TMP_Dropdown>();
            drop.ClearOptions();
            drop.AddOptions(new List<string> { "Ataque", "Agarre", "Defensa" });

            rondasDropdowns.Add(drop);
        }

        iniciarJuegoButton.gameObject.SetActive(true);
        instructions.gameObject.SetActive(true);
    }

    // Al presionar "Jugar"
    public void IniciarJuego()
    {
        SceneManager.LoadScene("LevelBackground");

        eleccionesRondas.Clear();
        foreach (TMP_Dropdown drop in rondasDropdowns)
        {
            string seleccion = drop.options[drop.value].text;
            eleccionesRondas.Add(seleccion);
        }

        Debug.Log("Selecciones registradas:");
        foreach (string opcion in eleccionesRondas)
        {
            Debug.Log(opcion);
        }

        // Oculta el panel de configuración
        setupPanel.SetActive(false);

        // Llama la corrutina directamente
        manager.IniciarJuegoDesdeRoundSetup();
    }
}
