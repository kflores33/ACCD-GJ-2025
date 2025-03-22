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
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        AddToStat(timeSurvived: _currentTime);

        if (_wizardBehavior == null) return;
        if (_wizardBehavior.CurrentState == K_WizardBehavior.WizardStates.Weak)
        {
            // Logic for when the wizard is weak
            arrowSpeed = arrowSpeed + Time.deltaTime / 2;
            // like...every so often, spawn an arrow

            if(_enemySpawnScript == null) return ;

            if (!_isSpawning)
            {
                _spawnStuffCoroutine = StartCoroutine(SpawnThing(isWeak: true));
            }
        }
        else if (_wizardBehavior.CurrentState == K_WizardBehavior.WizardStates.Strong)
        {
            // Logic for when the wizard is strong
            enemySpeed = enemySpeed + Time.deltaTime / 2;
            // every so often, spawn an enemy

            if (_enemySpawnScript == null) return;

            if (!_isSpawning)
            {
                _spawnStuffCoroutine = StartCoroutine(SpawnThing(isWeak: false));
            }
        }

        if((_last30sInterval + 30) <= _currentTime) // every 30s, increase speed
        {
            _30sHasPassed = true;
            IncreaseGameSpeed();
        }
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
        if (timesHit > 0) _scoreFactors.TimesHit += timesHit;
        if (enemiesKilled > 0) _scoreFactors.EnemiesKilled += enemiesKilled;
        if (timeSurvived > 0) _scoreFactors.TimeSurvived += timeSurvived;
    }

    [SerializeField] int _currentScore;
    [SerializeField] float _enemyKillMultiplier = 10;
    [SerializeField] float _timeSurvivedMultiplier = 1;
    [SerializeField] float _timesHitMultiplier = -5;
    public void ConvertToScore()
    {
        _currentScore = (int)((_scoreFactors.EnemiesKilled * _enemyKillMultiplier) + (_scoreFactors.TimeSurvived * _timeSurvivedMultiplier) + (_scoreFactors.TimesHit * _timesHitMultiplier));
    }
    #endregion
}
