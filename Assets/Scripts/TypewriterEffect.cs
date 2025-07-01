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
            yield return new WaitForSeconds(typingSpeed);
        }

        // Espera 1 segundo después de terminar de escribir
        yield return new WaitForSeconds(1f);

        // Carga la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
