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
    private float lastUpdateSecond;  // ���ڼ�¼��һ�����ӷ�����ʱ��
    public GameObject previousGameInfoPanel;  // ��ʾ�ϴ���Ϸ��Ϣ�����
    public TextMeshProUGUI previousGameInfoText;  // ��ʾ�ϴ���Ϸ��Ϣ���ı�

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
                    score += 5;  // ���ʱ�����60�룬ÿ��+5��
                }
                else
                {
                    score += 1;  // ���ʱ��С�ڻ����60�룬ÿ��+1��
                }
                lastUpdateSecond = Mathf.Floor(survivalTime);
                UpdateScoreText();
            }
        }
    }

    public void StartGameUI()
    {
        ResetUI();  // ���� UI ��������
        scoreText.gameObject.SetActive(true);  // ȷ���÷��ı��ǿɼ���
        survivalTimeText.gameObject.SetActive(true);  // ȷ������ʱ���ı��ǿɼ���
        UpdateScoreText();  // ��ʼ���÷���ʾ
        UpdateSurvivalTimeText();  // ��ʼ������ʱ����ʾ
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
            GameManager.Instance.StartGame();  // ����ȷ����Ϸ�ܹ���ʼ
        }//If no matching record is found, or it is the player's first time playing, the panel and previousGameInfoPanel are set to be invisible, and then GameManager.
         //Instance.StartGame() is called to start the game.
    }



    public void ResetUI()
    {
        nameInputField.text = "";  // ��������ֶ�
        nameInputField.gameObject.SetActive(true);  // ��ʾ�����ֶ�
        submitButton.gameObject.SetActive(true);  // ��ʾ�ύ��ť
        previousGameInfoPanel.SetActive(false);  // �����ϴ���Ϸ��Ϣ���
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
        panel.SetActive(true);  // ��ʾ�������ֵ����
        previousGameInfoPanel.SetActive(false);
        scoreText.gameObject.SetActive(false);
        survivalTimeText.gameObject.SetActive(false);
        Time.timeScale = 0;  // ȷ����Ϸ��ͣ
    }

}
