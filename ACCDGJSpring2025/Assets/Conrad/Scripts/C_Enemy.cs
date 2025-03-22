using UnityEngine;

public class C_Enemy : MonoBehaviour
{
    public PlayerMockup playerScript;
    public C_GameManagerMockup GMScript;
    public float monsterSpeed;
    public bool isDying;
    public int monsterSize;
    public BoxCollider monsterHitbox;
    public GameObject monsterShadow;
    public GameObject monsterSprite;

    public void Awake()
    {
        GMScript = FindFirstObjectByType<C_GameManagerMockup>();
        monsterSize = Random.Range(1, 4);
        if (monsterSize == 1)
        {
            monsterHitbox.size = new Vector3(1f,1f,1f);
            monsterShadow.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            monsterSprite.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            monsterSprite.transform.position = new Vector3(monsterSprite.transform.position.x, 0.53f, monsterSprite.transform.position.z);
        }
        if (monsterSize == 2)
        {
            monsterHitbox.size = new Vector3(2f, 2f, 2f);
            monsterShadow.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            monsterSprite.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        if (monsterSize == 3)
        {
            monsterHitbox.size = new Vector3(3f, 3f, 3f);
            monsterShadow.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            monsterSprite.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            monsterSprite.transform.position = new Vector3(monsterSprite.transform.position.x, 1.5f, monsterSprite.transform.position.z);
        }
    }

    public void Update()
    {
        monsterSpeed = GMScript.enemySpeed;
        this.transform.position = new Vector3(transform.position.x - monsterSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision col)
    {
        playerScript = col.gameObject.GetComponent<PlayerMockup>();
        if (playerScript != null)
        {
            if (!isDying)
            {
                isDying = true;
                Debug.Log("Hit Player");
                GMScript.numberOfKilledEnemies++;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
