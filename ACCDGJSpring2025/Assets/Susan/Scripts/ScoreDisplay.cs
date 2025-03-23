using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public K_GameManager gameManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private float elapsedTime = 0f;
    public int currentScore = 0;  // Local copy of the score

    void Update()
    {
        if (gameManager != null)
        {
            currentScore = gameManager.CurrentScore;
            scoreText.text = "Score: " + currentScore;
            elapsedTime += Time.deltaTime;
            timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime) + "s";
        }
    }
}
