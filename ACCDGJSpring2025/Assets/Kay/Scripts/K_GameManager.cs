using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

struct ScoreFactors
{
    public int TimesHit;
    public int EnemiesKilled;
    public float TimeSurvived;
}
public class K_GameManager : MonoBehaviour
{
    public string playerName;
    public int CurrentScore => _currentScore;
    public float CurrentTimeSurvived => _currentTime;

    K_WizardBehavior _wizardBehavior;
    C_EnemySpawnScript _enemySpawnScript;
    float _currentTime;

    public float arrowSpeed;
    public float enemySpeed;

    public float spawnSpeedMultiplier;

    ScoreFactors _scoreFactors;

    Coroutine _spawnStuffCoroutine;
    bool _isSpawning;

    void Start()
    {
        _enemySpawnScript = FindFirstObjectByType<C_EnemySpawnScript>();
        _wizardBehavior = FindFirstObjectByType<K_WizardBehavior>();
        Time.timeScale = 1; // Ensure the game is running

        _last30sInterval = 0;

        if (PlayerInfo.Instance != null)
        {
            playerName = PlayerInfo.Instance.playerName;
            InitializeGameWithPlayerName();
        }
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        while (_currentTime >= 1f)
        {
            AddToStat(timeSurvived: 1); // Increment the survival time by exactly one second
            _currentTime -= 1f; // Reduce current time by one second to account for the time added to stats
            ConvertToScore();  // Recalculate the score
        }

        HandleGameplayLogic();
    }

    void HandleGameplayLogic()
    {
        if (_wizardBehavior == null) return;
        if (_wizardBehavior.CurrentState == K_WizardBehavior.WizardStates.Weak)
        {
            AdjustArrowSpeed();
            TrySpawn(SpawnThing(isWeak: true));
        }
        else if (_wizardBehavior.CurrentState == K_WizardBehavior.WizardStates.Strong)
        {
            AdjustEnemySpeed();
            TrySpawn(SpawnThing(isWeak: false));
        }

        if ((_last30sInterval + 30) <= _currentTime)
        {
            _30sHasPassed = true;
            IncreaseGameSpeed();
        }
    }

    void AdjustArrowSpeed()
    {
        if (arrowSpeed < 20)
        {
            arrowSpeed += Time.deltaTime / 2;
        }
    }

    void AdjustEnemySpeed()
    {
        if (enemySpeed < 20)
        {
            enemySpeed += Time.deltaTime / 2;
        }
    }

    void TrySpawn(IEnumerator spawnRoutine)
    {
        if (_enemySpawnScript == null || _isSpawning) return;
        _spawnStuffCoroutine = StartCoroutine(spawnRoutine);
    }


    float _last30sInterval;
    bool _30sHasPassed;

    private void IncreaseGameSpeed()
    {
        spawnSpeedMultiplier += 0.5f;
        // increase enemy and arrow speed too, idk by what interval yet tho
        _last30sInterval = _currentTime;

        _30sHasPassed = false;
    }

    private IEnumerator SpawnThing(bool isWeak)
    {
        _isSpawning = true;
        yield return new WaitForSeconds(1 - (0.05f * spawnSpeedMultiplier));
        if (isWeak)
        {
            _enemySpawnScript.SpawnArrow();
        }
        else
        {
            _enemySpawnScript.SpawnEnemy();
        }
        _isSpawning = false;

        StopCoroutine(_spawnStuffCoroutine);
        _spawnStuffCoroutine = null;
    }

    #region Score Related
    public void AddToStat(int timesHit = 0, int enemiesKilled = 0, float timeSurvived = 0)
    {
        if (timesHit > 0)
        {
            // Adjust the score factor for times hit to decrease the score
            _scoreFactors.TimesHit += timesHit;
        }
        if (enemiesKilled > 0)
        {
            _scoreFactors.EnemiesKilled += enemiesKilled;
        }
        if (timeSurvived > 0)
        {
            _scoreFactors.TimeSurvived += timeSurvived;
        }

        ConvertToScore(); // Update score after modifying the stats
    }

    void InitializeGameWithPlayerName()
    {
        // Any initialization logic that depends on the player's name
        Debug.Log("Game started with player: " + playerName);
        // You can update any UI or game logic that needs the player's name here
    }





    [SerializeField] int _currentScore;
    [SerializeField] float _enemyKillMultiplier = 10;
    [SerializeField] float _timeSurvivedMultiplier = 1;
    [SerializeField] float _timesHitMultiplier = -5;
    public void ConvertToScore()
{
    int newScore = (int)(_scoreFactors.EnemiesKilled * _enemyKillMultiplier +
                         _scoreFactors.TimeSurvived * _timeSurvivedMultiplier +
                         _scoreFactors.TimesHit * _timesHitMultiplier);

    // Check if the new score calculation results in a negative score and the current score is zero
    if (_currentScore == 0 && newScore < 0)
    {
        // Do not allow the score to go negative if it's already zero
        _currentScore = 0;
    }
    else
    {
        _currentScore = Mathf.Max(0, newScore);  // Prevent the score from going negative
    }
}


    #endregion
}
