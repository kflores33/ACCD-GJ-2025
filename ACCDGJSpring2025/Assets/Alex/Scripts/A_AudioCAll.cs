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

    //Use this to replace the SFXfunction() in the instruction
    public void Musicfunction(AudioClip clip)
    {
        music.PlayOneShot(clip);
    }

    public void SFXfunction(AudioClip clip)
    {
        sFX.PlayOneShot(clip);
    }

    //Use this to replace the SFXfunction() in the instruction
    public void Dialogfunction(AudioClip clip)
    {
        dialog.PlayOneShot(clip);
    }

}
