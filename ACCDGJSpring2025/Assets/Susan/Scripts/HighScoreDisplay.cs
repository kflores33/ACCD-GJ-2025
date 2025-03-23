using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text = "Top 5 High Scores:\n";
        for (int i = 1; i <= 5; i++)
        {
            string name = PlayerPrefs.GetString($"HighScore{i}Name", "");
            int score = PlayerPrefs.GetInt($"HighScore{i}Score", 0);
            if (!string.IsNullOrEmpty(name))  // Only display scores that exist
            {
                highScoreText.text += $"{i}. {name} - {score}\n";
            }
        }
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");  // Replace "TitleScene" with the actual name of your title scene
    }
}
