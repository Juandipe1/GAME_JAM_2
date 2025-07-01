using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] string[] choises;
    [SerializeField] int numberRounds;

    int buttomPresed = 0;

    List<string> playerChoises = new List<string>();
    List<string> cpuChoises = new List<string>();

    public void Play(string myChoise)
    {
        if (buttomPresed < numberRounds)
        {
            string cpuChoise = choises[Random.Range(0, choises.Length)];

            playerChoises.Add(myChoise);
            cpuChoises.Add(cpuChoise);

            buttomPresed++;

            if (buttomPresed == numberRounds)
            {
                ShowResults();
                Debug.Log("All rounds have been played.");
            }
        }
    }

    private void ShowResults()
    {
        Debug.Log("====RESULTS====");

        for (int i = 0; i < numberRounds; i++)
        {
            Debug.Log($"Round {i + 1}: Player: {playerChoises[i]} | CPU: {cpuChoises[i]}");
            if (playerChoises[i] == cpuChoises[i])
            {
                Debug.Log("Empate");
            }

            if ((cpuChoises[i] == "Attack" && playerChoises[i] == "Defense") ||
                (cpuChoises[i] == "Defense" && playerChoises[i] == "Grab") ||
                (cpuChoises[i] == "Grab" && playerChoises[i] == "Attack"))
            {
                Debug.Log("Ganaste");
            }
            else
            {
                Debug.Log("Perdiste");
            }
        }
    }
}
