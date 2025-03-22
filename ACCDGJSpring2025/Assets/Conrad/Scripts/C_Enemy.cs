using UnityEngine;
using UnityEngine.UI;

public class C_Enemy : MonoBehaviour
{
    public PlayerMockup playerScript;
    public C_GameManagerMockup GMScript;
    public K_WizardBehavior K_Wiz;

    public K_GameManager k_GM; // added my game manager script, but yours should still work!

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
        k_GM = FindFirstObjectByType<K_GameManager>();
        monsterSize.transform.localScale = new Vector3(MonsterSO.enemySize, MonsterSO.enemySize, MonsterSO.enemySize);
        monsterReward = MonsterSO.enemyReward;
        monsterSprite = MonsterSO.enemySprite;
        monsterDamage = MonsterSO.enemyDamage;
    }

    public void Update()
    {
        if(k_GM != null) monsterSpeed = k_GM.enemySpeed;
        //else if (k_GameManager != null) monsterSpeed = k_GameManager.enemySpeed;
        this.transform.position = new Vector3(transform.position.x - monsterSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision col)
    {
        K_Wiz = col.gameObject.GetComponent<K_WizardBehavior>();
        if (K_Wiz != null)
        {
            if (!isDying)
            {
                isDying = true;
                Debug.Log("Hit Player");
                if (GMScript != null) GMScript.numberOfKilledEnemies++;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
