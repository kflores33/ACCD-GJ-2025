using UnityEngine;

public class C_Arrow : MonoBehaviour
{
    public PlayerMockup playerScript;
    public C_GameManagerMockup GMScript;
    public float arrowSpeed;
    public bool isDying;
    private void Awake()
    {
        GMScript = FindObjectOfType<C_GameManagerMockup>();
    }
    void Update()
    {
        arrowSpeed = GMScript.arrowSpeed;
        this.transform.position = new Vector3(transform.position.x - arrowSpeed * Time.deltaTime, transform.position.y, transform.position.z);
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
                GMScript.timesHitByArrow++;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
