using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public K_GameManager gameManager; // Reference to the GameManager component
    public TextMeshProUGUI scoreText; // UI Text element for displaying the score
    public TextMeshProUGUI timeText;  // UI Text element for displaying the time

    private float elapsedTime = 0f;  // Local timer for tracking elapsed time

    void Update()
    {
        if (gameManager != null)
        {
            // Update the score from the game manager
            scoreText.text = "Score: " + gameManager.CurrentScore;

            // Increment the local timer based on real time elapsed
            elapsedTime += Time.deltaTime;
            // Display the elapsed time rounded down to the nearest second
            timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString() + "s";
        }
    }
}
