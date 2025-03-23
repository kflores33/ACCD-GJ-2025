using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;

    public TMP_InputField nameInputField;
    public Button submitButton;
    public string playerName;
    public int currentScore;
    public List<int> highScores = new List<int>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);  // This object persists across scenes
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
