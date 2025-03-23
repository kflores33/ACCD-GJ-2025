using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
public class A_SoundManager : MonoBehaviour
{
    public RectTransform OptionsPanel;
    public Vector2 OptionsPanelHidden;
    Vector2 _onScreenPos;

    private AudioMixer audioMixer;

    private float minValueVolume;
    private Slider masterSlider;
    private Slider musicSlider;
    private Slider SFXSlider;
    private Slider dialogSlider;

    const string mixer_Master = "MasterVolume";
    const string mixer_Music = "MusicVolume";
    const string mixer_SFX = "SFXVolume";
    const string mixer_Dialog = "DialogVolume";
    
    void Awake()
    {
        _onScreenPos = Vector2.zero;
        OptionsPanel.anchoredPosition = OptionsPanelHidden;
        audioMixer = Resources.Load<AudioMixer>("Mixer");

        masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        SFXSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        dialogSlider = GameObject.Find("DialogSlider").GetComponent<Slider>();

        minValueVolume = 0.0001f;

        masterSlider.minValue = minValueVolume;
        musicSlider.minValue = minValueVolume;
        SFXSlider.minValue = minValueVolume;
        dialogSlider.minValue = minValueVolume;

        // I (kay) added some stuff to store the values between scenes/plays/whatever
        if (PlayerPrefs.HasKey("MasterVolume")) {
            //Debug.Log("master volume has been set previously");
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume")); 
        }
        if (PlayerPrefs.HasKey("MusicVolume")) { 
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
        if (PlayerPrefs.HasKey("DialogVolume"))
        {
            dialogSlider.value = PlayerPrefs.GetFloat("DialogVolume");
            SetDialogVolume(PlayerPrefs.GetFloat("DialogVolume"));
        }

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        dialogSlider.onValueChanged.AddListener(SetDialogVolume);
    }

    void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(mixer_Master, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.Save();
    }
    void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(mixer_Music, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }
    void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(mixer_SFX, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.Save();
    }
    void SetDialogVolume(float value)
    {
        audioMixer.SetFloat(mixer_Dialog, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("DialogVolume", dialogSlider.value);
        PlayerPrefs.Save();
    }
    public void MoveOptionsPanelOffScreen()
    {
        OptionsPanel = GameObject.Find("OptionsMenu").GetComponent<RectTransform>();
        OptionsPanel.anchoredPosition = OptionsPanelHidden;
    }
    public void MoveOptionsPanelOnScreen()
    {
        //Debug.Log("panel should move");
        OptionsPanel = GameObject.Find("OptionsMenu").GetComponent<RectTransform>();
        OptionsPanel.anchoredPosition = _onScreenPos;
    }
}