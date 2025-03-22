using UnityEngine;

public class C_EnemySpawnScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject enemyPrefab;
    public float maxRange;
    public float minRange;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange,maxRange));
            SpawnArrow();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange, maxRange));
            SpawnEnemy();
        }
    }

    public void SpawnArrow()
    {
        Instantiate(arrowPrefab,this.transform.position,Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
    }
}
