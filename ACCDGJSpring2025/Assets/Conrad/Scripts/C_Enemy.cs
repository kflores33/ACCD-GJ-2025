using UnityEngine;
using UnityEngine.UI;

public class C_Enemy : MonoBehaviour
{
    public PlayerMockup playerScript;
    public C_GameManagerMockup GMScript;
    public float monsterSpeed;
    public bool isDying;
    public GameObject monsterSize;
    public float monsterReward;
    public SpriteRenderer monsterSR;
    public Sprite monsterSprite;
    public float monsterDamage;
    public EnemySO MonsterSO;

    public void Awake()
    {
        GMScript = FindFirstObjectByType<C_GameManagerMockup>();
        monsterSize.transform.localScale = new Vector3(MonsterSO.enemySize, MonsterSO.enemySize, MonsterSO.enemySize);
        monsterReward = MonsterSO.enemyReward;
        monsterSprite = MonsterSO.enemySprite;
        monsterDamage = MonsterSO.enemyDamage;
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
