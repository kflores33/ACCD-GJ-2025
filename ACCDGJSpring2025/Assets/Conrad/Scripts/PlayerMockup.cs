using Unity.VisualScripting;
using UnityEngine;

public class PlayerMockup : MonoBehaviour
{
    public C_GameManagerMockup GMMockup;
    public float playSpeed;
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + playSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - playSpeed * Time.deltaTime);
        }

    }
}
