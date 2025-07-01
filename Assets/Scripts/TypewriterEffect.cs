using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class type : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    [TextArea(4, 20)]
    public string fullText;

    public float typingSpeed = 0.05f;
    public float scrollSpeed = 20f;
    public string nextSceneName = "MainMenu"; // ← nombre exacto de la escena

    public AudioSource audioSource;     // ← Asigna esto desde el Inspector
    public AudioClip typingSound;       // ← Clip corto tipo tecla

    private RectTransform textRect;

    void Start()
    {
        textRect = textUI.GetComponent<RectTransform>();
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        textUI.text = "";
        foreach (char c in fullText)
        {
            textUI.text += c;
            textRect.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

            // Reproducir sonido solo para letras o números
            if (char.IsLetterOrDigit(c) && audioSource != null && typingSound != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        // Espera 1 segundo después de terminar de escribir
        yield return new WaitForSeconds(1f);

        // Carga la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
