using UnityEngine;

public class K_Title : MonoBehaviour
{
    public AudioClip Music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameObject.Find("Music").GetComponent<AudioSource>().clip != null)
        {
            GameObject.Find("Music").GetComponent<AudioSource>().clip = null;
        }
        A_AudioCAll.instance.Musicfunction(Music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
