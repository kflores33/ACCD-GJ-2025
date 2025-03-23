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
            // Store the score and player's name using PlayerPrefs
            PlayerPrefs.SetInt("LastGameScore", gameManager.CurrentScore);
            PlayerPrefs.SetString("PlayerName", PlayerInfo.Instance.playerName);
        }
        else
        {
            Debug.LogError("GameManager is not set or available.");
            PlayerPrefs.SetInt("LastGameScore", 0);
            PlayerPrefs.SetString("PlayerName", "Unknown Player");
        }

        SceneManager.LoadScene("EndScene");
    }
}
