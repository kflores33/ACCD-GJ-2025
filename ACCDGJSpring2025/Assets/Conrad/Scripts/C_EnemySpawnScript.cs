using UnityEngine;

public class C_EnemySpawnScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float maxRange;
    public float minRange;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange,maxRange));
            SpawnArrow();
        }
        if (Input.GetKey(KeyCode.G))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(minRange, maxRange));
            SpawnArrow();
        }
    }

    public void SpawnArrow()
    {
        Instantiate(arrowPrefab,this.transform.position,Quaternion.identity);
    }
}
