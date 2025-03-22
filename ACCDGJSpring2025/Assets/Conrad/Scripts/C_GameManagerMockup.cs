using System.Collections;
using UnityEngine;

public class C_GameManagerMockup : MonoBehaviour
{
    public float timesHitByArrow;
    public float arrowSpeed;
    public float enemySpeed;
    public float numberOfKilledEnemies;
    public bool isWeak;
    public bool canSpawn = true;
    public float spawnInterval;
    public C_EnemySpawnScript spawnerScript;

    public void Update()
    {
        if (isWeak)
        {
            arrowSpeed = arrowSpeed + Time.deltaTime / 2;
            if (Input.GetKeyDown(KeyCode.H))
            {
                isWeak = false;
            }
        }
        else
        {
            enemySpeed = enemySpeed + Time.deltaTime / 2;
            if (Input.GetKeyDown(KeyCode.H))
            {
                isWeak = true;
            }
        }
        if (canSpawn)
        {
            StartCoroutine(thingSpawn());

            if (spawnInterval > 0.1f)
            {
                spawnInterval = spawnInterval - Time.deltaTime;
            }
        }

        IEnumerator thingSpawn()
        {
            canSpawn = false;
            if (isWeak)
            {
                spawnerScript.SpawnArrow();
            }
            else
            {
                spawnerScript.SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
            canSpawn = true;
            StopCoroutine(thingSpawn());
        }
    }
}
