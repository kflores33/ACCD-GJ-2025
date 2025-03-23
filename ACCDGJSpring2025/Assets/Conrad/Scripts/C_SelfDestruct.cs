using System.Collections;
using UnityEngine;

public class C_SelfDestruct : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Self());
    }

    IEnumerator Self()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
