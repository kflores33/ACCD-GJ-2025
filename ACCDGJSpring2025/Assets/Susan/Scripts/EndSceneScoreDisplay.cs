using UnityEngine;
using TMPro;

public class EndSceneScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // Fetch the score and player's name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "[Name not found]");
        int finalScore = PlayerPrefs.GetInt("LastGameScore", 0);

        playerNameText.text = "Player: " + playerName;
        finalScoreText.text = "Final Score: " + finalScore;
    }
}
