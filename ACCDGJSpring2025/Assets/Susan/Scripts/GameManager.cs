using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // 单例模式

    public GameObject gameEndPanel;  // 游戏结束面板
    public TextMeshProUGUI gameEndText;  // 游戏结束显示文本
    public List<(string name, int score)> highScores = new List<(string, int)>();  // 高分列表
    public GameObject previousGameInfoPanel;  // 显示上次游戏信息的面板

    private float gameTimer = 0;
    private bool isGameRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 使对象持久化
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        previousGameInfoPanel.SetActive(false);  // 确保游戏开始时隐藏previous game info面板
        gameEndPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameRunning)
        {
            gameTimer += Time.deltaTime;

            if (gameTimer >= 60.0f)  // 60秒后游戏结束
            {
                EndGame();
            }
        }
    }
    public void StartGame()
    {
        isGameRunning = true;
        gameTimer = 0;
        Time.timeScale = 1;  // 确保游戏时间恢复正常流动
        FindObjectOfType<NameInput>().StartGameUI();  // 确保 UI 正确设置
    }

    public void RestartGame()
    {
        gameEndPanel.SetActive(false);
        ResetGameState();
        ShowInputPanel();  // 显示输入名字的面板而不是直接开始游戏
    }

    public void ShowInputPanel()
    {
        var nameInputScript = FindObjectOfType<NameInput>();
        nameInputScript.ResetGame();  // 这个方法已经设置了 Time.timeScale = 0
    }


    public void ResetGameState()
    {
        var nameInputScript = FindObjectOfType<NameInput>();
        nameInputScript.ResetGame();
    }



    public void EndGame()
    {
        isGameRunning = false;
        Time.timeScale = 0;  // 暂停游戏时间
        gameEndPanel.SetActive(true);
        var playerName = FindObjectOfType<NameInput>().playerName;
        var playerScore = FindObjectOfType<NameInput>().score;
        gameEndText.text = "Game Over\n" + playerName + ": " + playerScore;
        UpdateHighScores(playerName, playerScore);
    }


    void UpdateHighScores(string playerName, int playerScore)
    {
        highScores.Add((playerName, playerScore));
        highScores.Sort((a, b) => b.score.CompareTo(a.score));  // 根据分数排序，分数高的在前
    }
}
