using UnityEngine;

public class A_Test : MonoBehaviour
{
    public AudioClip clip1;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            A_AudioCAll.instance.SFXfunction(clip1);
        }
    }
}
