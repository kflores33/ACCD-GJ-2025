using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button submitButton;
    public GameObject panel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI survivalTimeText;
    public string playerName;
    public int score;
    private float survivalTime;
    private float lastUpdateSecond;  // 用于记录上一次增加分数的时间
    public GameObject previousGameInfoPanel;  // 显示上次游戏信息的面板
    public TextMeshProUGUI previousGameInfoText;  // 显示上次游戏信息的文本

    void Start()
    {
        submitButton.onClick.AddListener(SubmitName);
        score = 0;
        survivalTime = 0;
        lastUpdateSecond = 0;
        Time.timeScale = 0;
        scoreText.gameObject.SetActive(false);
        survivalTimeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            survivalTime += Time.deltaTime;
            UpdateSurvivalTimeText();
            if (Mathf.Floor(survivalTime) > lastUpdateSecond)
            {
                if (survivalTime > 30)
                {
                    score += 5;  // 存活时间大于60秒，每秒+5分
                }
                else
                {
                    score += 1;  // 存活时间小于或等于60秒，每秒+1分
                }
                lastUpdateSecond = Mathf.Floor(survivalTime);
                UpdateScoreText();
            }
        }
    }

    public void StartGameUI()
    {
        ResetUI();  // 重置 UI 输入面板等
        scoreText.gameObject.SetActive(true);  // 确保得分文本是可见的
        survivalTimeText.gameObject.SetActive(true);  // 确保生存时间文本是可见的
        UpdateScoreText();  // 初始化得分显示
        UpdateSurvivalTimeText();  // 初始化生存时间显示
    }


    void SubmitName()
    {
        playerName = nameInputField.text;
        var lastGame = GameManager.Instance.highScores.Find(p => p.name == playerName);
        //SubmitName method first retrieves the high score list from GameManager and tries to find a name that matches the player's entered name.
        if (lastGame.name != null && lastGame.name == playerName)
        {
            previousGameInfoText.text = "Last Time: Score: " + lastGame.score;
            previousGameInfoPanel.SetActive(true);
        }//If a matching record is found (lastGame is not null and lastGame.name equals the entered playerName),
         //the text of previousGameInfoText is set to display that record's score.
        else
        {
            panel.SetActive(false);
            previousGameInfoPanel.SetActive(false);
            GameManager.Instance.StartGame();  // 这行确保游戏能够开始
        }//If no matching record is found, or it is the player's first time playing, the panel and previousGameInfoPanel are set to be invisible, and then GameManager.
         //Instance.StartGame() is called to start the game.
    }



    public void ResetUI()
    {
        nameInputField.text = "";  // 清除输入字段
        nameInputField.gameObject.SetActive(true);  // 显示输入字段
        submitButton.gameObject.SetActive(true);  // 显示提交按钮
        previousGameInfoPanel.SetActive(false);  // 隐藏上次游戏信息面板
    }


    void UpdateScoreText()
    {
        scoreText.text = playerName + " Score: " + score.ToString();
    }

    void UpdateSurvivalTimeText()
    {
        survivalTimeText.text = playerName + " Survival Time: " + Mathf.FloorToInt(survivalTime).ToString() + "s";
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void StartNewGame()
    {
        previousGameInfoPanel.SetActive(false);
        panel.SetActive(false);
        GameManager.Instance.StartGame();
    }
    public void ResetGame()
    {
        score = 0;
        survivalTime = 0;
        lastUpdateSecond = 0;
        playerName = "";
        nameInputField.text = "";
        panel.SetActive(true);  // 显示输入名字的面板
        previousGameInfoPanel.SetActive(false);
        scoreText.gameObject.SetActive(false);
        survivalTimeText.gameObject.SetActive(false);
        Time.timeScale = 0;  // 确保游戏暂停
    }

}
