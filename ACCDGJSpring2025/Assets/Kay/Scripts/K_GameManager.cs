using UnityEngine;
using System.Collections.Generic;

struct ScoreFactors
{
    public int TimesHit;
    public int EnemiesKilled;
    public float TimeSurvived;
}
public class K_GameManager : MonoBehaviour
{
    K_WizardBehavior _wizardBehavior;
    float _currentTime;

    public float arrowSpeed;

    ScoreFactors _scoreFactors;

    void Start()
    {
        _wizardBehavior = FindFirstObjectByType<K_WizardBehavior>();
        Time.timeScale = 1; // Ensure the game is running
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        AddToStat(timeSurvived: _currentTime);

        if (_wizardBehavior == null) return;
        if (_wizardBehavior.CurrentState == K_WizardBehavior.WizardStates.Weak)
        {
            // Logic for when the wizard is weak
            // like...every so often, spawn an arrow
        }
        else if (_wizardBehavior.CurrentState == K_WizardBehavior.WizardStates.Strong)
        {
            // Logic for when the wizard is strong
            // every so often, spawn an enemy
        }
    }

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
}
