using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // ����ģʽ

    public GameObject gameEndPanel;  // ��Ϸ�������
    public TextMeshProUGUI gameEndText;  // ��Ϸ������ʾ�ı�
    public List<(string name, int score)> highScores = new List<(string, int)>();  // �߷��б�
    public GameObject previousGameInfoPanel;  // ��ʾ�ϴ���Ϸ��Ϣ�����

    private float gameTimer = 0;
    private bool isGameRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ʹ����־û�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        previousGameInfoPanel.SetActive(false);  // ȷ����Ϸ��ʼʱ����previous game info���
        gameEndPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameRunning)
        {
            gameTimer += Time.deltaTime;

            if (gameTimer >= 60.0f)  // 60�����Ϸ����
            {
                EndGame();
            }
        }
    }
    public void StartGame()
    {
        isGameRunning = true;
        gameTimer = 0;
        Time.timeScale = 1;  // ȷ����Ϸʱ��ָ���������
        FindObjectOfType<NameInput>().StartGameUI();  // ȷ�� UI ��ȷ����
    }

    public void RestartGame()
    {
        gameEndPanel.SetActive(false);
        ResetGameState();
        ShowInputPanel();  // ��ʾ�������ֵ���������ֱ�ӿ�ʼ��Ϸ
    }

    public void ShowInputPanel()
    {
        var nameInputScript = FindObjectOfType<NameInput>();
        nameInputScript.ResetGame();  // ��������Ѿ������� Time.timeScale = 0
    }


    public void ResetGameState()
    {
        var nameInputScript = FindObjectOfType<NameInput>();
        nameInputScript.ResetGame();
    }



    public void EndGame()
    {
        isGameRunning = false;
        Time.timeScale = 0;  // ��ͣ��Ϸʱ��
        gameEndPanel.SetActive(true);
        var playerName = FindObjectOfType<NameInput>().playerName;
        var playerScore = FindObjectOfType<NameInput>().score;
        gameEndText.text = "Game Over\n" + playerName + ": " + playerScore;
        UpdateHighScores(playerName, playerScore);
    }


    void UpdateHighScores(string playerName, int playerScore)
    {
        highScores.Add((playerName, playerScore));
        highScores.Sort((a, b) => b.score.CompareTo(a.score));  // ���ݷ������򣬷����ߵ���ǰ
    }
}
