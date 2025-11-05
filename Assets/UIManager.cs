using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button respawnButton;

    private PlayerInteraction playerInteraction;

    void Start()
    {
        fadeCanvas.alpha = 0f;
        gameOverText.gameObject.SetActive(false);
        respawnButton.gameObject.SetActive(false);
        respawnButton.onClick.AddListener(OnRespawnClicked);
    }

    public void SetPlayer(PlayerInteraction player)
    {
        playerInteraction = player;
    }

    public void ShowGameOver()
    {
        StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeInSequence()
    {
        // Active le canvas
        gameObject.SetActive(true);

        // Fade in progressif
        float duration = 1.5f;
        float t = 0f;

        while (t < duration)
        {
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        fadeCanvas.alpha = 1f;

        // Montre le texte et le bouton
        gameOverText.gameObject.SetActive(true);
        respawnButton.gameObject.SetActive(true);
    }

    private void OnRespawnClicked()
    {
        // Cache tout
        fadeCanvas.alpha = 0f;
        gameOverText.gameObject.SetActive(false);
        respawnButton.gameObject.SetActive(false);
        gameObject.SetActive(false);

        if (playerInteraction != null)
        {
            playerInteraction.Respawn();
        }

        StartCoroutine(FadeOutSequence());
    }

    private IEnumerator FadeOutSequence()
    {
        float duration = 1.5f;
        float t = 0f;

        while (t < duration)
        {
            fadeCanvas.alpha = Mathf.Lerp(1f, 0f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        fadeCanvas.alpha = 0f;

        // Cache complètement le canvas après le fade
        gameObject.SetActive(false);
    }
}
