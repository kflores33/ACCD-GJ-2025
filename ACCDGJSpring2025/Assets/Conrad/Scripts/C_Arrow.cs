using UnityEngine;

public class C_Arrow : MonoBehaviour
{
    public PlayerMockup playerScript;
    public C_GameManagerMockup GMScript;

    public K_GameManager k_GM; // added my game manager script, but yours should still work!

    public float arrowSpeed;
    public bool isDying;
    public GameObject painParticle;

    private void Awake()
    {
        GMScript = FindFirstObjectByType<C_GameManagerMockup>();
        k_GM = FindFirstObjectByType<K_GameManager>();
    }
    //Move arrow forwards at the rate determined by the game manager
    void Update()
    {
        if (k_GM != null) arrowSpeed = k_GM.arrowSpeed;
        //else if(k_GameManager != null) arrowSpeed = k_GameManager.arrowSpeed;

        this.transform.position = new Vector3(transform.position.x - arrowSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    //if the player is hit, 
    private void OnCollisionEnter(Collision col)
    {
        playerScript = col.gameObject.GetComponent<PlayerMockup>();
        if (playerScript != null)
        {
            if (!isDying)
            {
                Instantiate(painParticle, transform.position, Quaternion.identity);
                isDying = true;
                Debug.Log("Hit Player");
                if (GMScript != null) GMScript.timesHitByArrow++;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
