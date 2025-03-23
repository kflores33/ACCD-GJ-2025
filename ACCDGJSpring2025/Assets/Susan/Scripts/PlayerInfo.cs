using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;

    public TMP_InputField nameInputField;
    public Button submitButton;
    public string playerName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        submitButton.onClick.AddListener(SubmitName);
    }

    void SubmitName()
    {
        playerName = nameInputField.text;  // Store the player's name
        SceneManager.LoadScene("Conrad_TestScene");  // Load the game scene
    }
}
