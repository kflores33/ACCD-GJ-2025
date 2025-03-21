using UnityEngine;

public class C_GameManagerMockup : MonoBehaviour
{
    public float timesHitByArrow;
    public float arrowSpeed;
    public float enemySpeed;
    public float numberOfKilledEnemies;
    public bool isWeak;

    public void Update()
    {
        if (isWeak)
        {
            arrowSpeed = arrowSpeed + Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.G))
            {
                isWeak = false;
            }
        }
        else
        {
            enemySpeed = enemySpeed + Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.G))
            {
                isWeak = false;
            }
        }
    }
}
