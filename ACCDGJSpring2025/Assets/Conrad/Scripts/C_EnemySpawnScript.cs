using UnityEngine;

public class C_EnemySpawnScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject shrimpyPrefab;
    public GameObject midPrefab;
    public GameObject hypePrefab;
    public float maxRange;
    public float minRange;
    public float monsterSize;
    public K_GameManager gameManager;
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange,maxRange));
    //        SpawnArrow();
    //    }
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange, maxRange));
    //        SpawnEnemy();
    //    }
    //}

    public void SpawnArrow()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange, maxRange));
        Instantiate(arrowPrefab,this.transform.position,Quaternion.identity);
    }

    public void SpawnEnemy()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange, maxRange));
        monsterSize = Random.Range(1,4);
        if (monsterSize == 1)
        {
            Instantiate(shrimpyPrefab, this.transform.position, Quaternion.identity);
        }
        if (monsterSize == 2)
        {
            Instantiate(midPrefab, this.transform.position, Quaternion.identity);
        }
        if (monsterSize == 3)
        {
            Instantiate(hypePrefab, this.transform.position, Quaternion.identity);
        }
    }
}
