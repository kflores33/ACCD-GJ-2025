using UnityEngine;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;

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
}
