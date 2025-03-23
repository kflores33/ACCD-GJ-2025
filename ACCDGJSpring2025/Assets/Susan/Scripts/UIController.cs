using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button submitButton;

    void Start()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(SubmitName);
        }
        else
        {
            Debug.LogError("Submit Button is not assigned in the inspector");
        }
    }

    void SubmitName()
    {
        if (PlayerInfo.Instance != null && nameInputField != null)
        {
            PlayerInfo.Instance.playerName = nameInputField.text;  // Store the player's name
            SceneManager.LoadScene("Conrad_TestScene");  // Load the game scene
        }
        else
        {
            Debug.LogError("PlayerInfo instance or name input field is not available");
        }
    }
}
