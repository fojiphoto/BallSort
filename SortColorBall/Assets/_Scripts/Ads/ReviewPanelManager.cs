using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPanelManager : MonoBehaviour
{
    public GameObject reviewPanel; // Reference to the review panel UI
    public Button reviewButton; // Reference to the review button
    public Button closeButton; // Reference to the close button

    private float timer = 0f;
    private bool hasReviewed = false;
    private bool panelActive = false;
    private float reviewInterval = 7 * 60f; // 7 minutes in seconds

    void Start()
    {
        hasReviewed = PlayerPrefs.GetInt(nameof(hasReviewed), 0) == 1;
        reviewPanel.SetActive(false); // Initially hide the panel
        reviewButton.onClick.AddListener(OnReviewButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    void Update()
    {
        if (!hasReviewed && !panelActive)
        {
            timer += Time.deltaTime;

            if (timer >= reviewInterval)
            {
                ShowReviewPanel();
            }
        }
    }

    void ShowReviewPanel()
    {
        reviewPanel.SetActive(true); // Show the review panel
        panelActive = true;
        //Time.timeScale = 0f; // Optional: Pause the game when the panel shows
    }

    void OnReviewButtonClick()
    {
        hasReviewed = true;
        PlayerPrefs.SetInt(nameof(hasReviewed),1);
        reviewPanel.SetActive(false); // Hide the panel
        panelActive = false;
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ogg.guessthings.quiz.games&hl=en");
        Time.timeScale = 1f; // Optional: Resume the game
    }

    void OnCloseButtonClick()
    {
        reviewPanel.SetActive(false); // Hide the panel
        panelActive = false;
        timer = 0f; // Reset the timer for another 7 minutes
        Time.timeScale = 1f; // Optional: Resume the game
    }
}
