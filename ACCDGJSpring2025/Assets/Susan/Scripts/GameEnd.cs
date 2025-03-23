using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public K_WizardBehavior wizard;  // Reference to the WizardBehavior script
    public K_GameManager gameManager;  // Reference to the GameManager

    void Update()
    {
        if (wizard == null || gameManager == null)
        {
            Debug.LogError("Dependencies not set on GameEndManager.");
            return;
        }

        if (wizard.currentMana <= 0)
        {
            TriggerEndGame();
        }
    }

    void TriggerEndGame()
    {
        if (gameManager != null)
        {
            int currentScore = gameManager.CurrentScore;
            string playerName = PlayerInfo.Instance.playerName;

            UpdateHighScores(currentScore, playerName);

            // Store the current game score and player's name
            PlayerPrefs.SetInt("LastGameScore", currentScore);
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            Debug.LogError("GameManager is not set or available.");
            PlayerPrefs.SetInt("LastGameScore", 0);
            PlayerPrefs.SetString("PlayerName", "Unknown Player");
        }

        SceneManager.LoadScene("EndScene");
    }

    void UpdateHighScores(int score, string playerName)
    {
        for (int i = 1; i <= 5; i++)
        {
            // Construct PlayerPrefs keys
            string scoreKey = $"HighScore{i}Score";
            string nameKey = $"HighScore{i}Name";

            if (PlayerPrefs.HasKey(scoreKey))
            {
                int savedScore = PlayerPrefs.GetInt(scoreKey);
                if (score > savedScore)
                {
                    // Shift down the leaderboard
                    for (int j = 5; j > i; j--)
                    {
                        int lowerScore = PlayerPrefs.GetInt($"HighScore{j - 1}Score");
                        string lowerName = PlayerPrefs.GetString($"HighScore{j - 1}Name");
                        PlayerPrefs.SetInt($"HighScore{j}Score", lowerScore);
                        PlayerPrefs.SetString($"HighScore{j}Name", lowerName);
                    }

                    // Insert new high score
                    PlayerPrefs.SetInt(scoreKey, score);
                    PlayerPrefs.SetString(nameKey, playerName);
                    break;
                }
            }
            else
            {
                // If no high score exists at this position, insert here
                PlayerPrefs.SetInt(scoreKey, score);
                PlayerPrefs.SetString(nameKey, playerName);
                break;
            }
        }
    }
}
