using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ConfigurationOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void FullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Volume(float volume){
        audioMixer.SetFloat("Volume", volume);
    }
}
