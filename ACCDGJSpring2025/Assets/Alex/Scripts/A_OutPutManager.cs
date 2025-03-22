using UnityEngine;
using UnityEngine.Audio;

public class A_OutPutManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioMixer audioMixer;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioMixer = Resources.Load<AudioMixer>("Mixer");

        if(this.gameObject.name == "Music")
        {
            AudioMixerGroup[] groups = audioMixer.FindMatchingGroups("Master/Music");
            audioSource.outputAudioMixerGroup = groups[0];
        }
        if(this.gameObject.name == "SFX")
        {
            AudioMixerGroup[] groups = audioMixer.FindMatchingGroups("Master/SFX");
            audioSource.outputAudioMixerGroup = groups[0];
        }
        if(this.gameObject.name == "Dialog")
        {
            AudioMixerGroup[] groups = audioMixer.FindMatchingGroups("Master/Dialog");
            audioSource.outputAudioMixerGroup = groups[0];
        }

    }


}
