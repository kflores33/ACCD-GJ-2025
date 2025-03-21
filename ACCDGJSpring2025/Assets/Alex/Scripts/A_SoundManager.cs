using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
public class A_SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private float minValueVolume;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider dialogSlider;

    const string mixer_Master = "MasterVolume";
    const string mixer_Music = "MusicVolume";
    const string mixer_SFX = "SFXVolume";
    const string mixer_Dialog = "DialogVolume";
    
    void Awake()
    {
        minValueVolume = 0.0001f;

        masterSlider.minValue = minValueVolume;
        musicSlider.minValue = minValueVolume;
        SFXSlider.minValue = minValueVolume;
        dialogSlider.minValue = minValueVolume;

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        dialogSlider.onValueChanged.AddListener(SetDialogVolume);
    }

    void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(mixer_Master, Mathf.Log10(value) * 20);
    }
    void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(mixer_Music, Mathf.Log10(value) * 20);
    }
    void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(mixer_SFX, Mathf.Log10(value) * 20);
    }
    void SetDialogVolume(float value)
    {
        audioMixer.SetFloat(mixer_Dialog, Mathf.Log10(value) * 20);
    }
}