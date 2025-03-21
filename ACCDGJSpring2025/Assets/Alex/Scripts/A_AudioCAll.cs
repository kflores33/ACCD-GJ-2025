using UnityEngine;

public class A_AudioCAll : MonoBehaviour
{
    public static A_AudioCAll instance;

    public AudioSource music;
    public AudioSource sFX;
    public AudioSource dialog;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    public void Musicfunction(AudioClip clip)
    {
        music.PlayOneShot(clip);
    }

    public void SFXfunction(AudioClip clip)
    {
        sFX.PlayOneShot(clip);
    }

    public void Dialogfunction(AudioClip clip)
    {
        dialog.PlayOneShot(clip);
    }

}
