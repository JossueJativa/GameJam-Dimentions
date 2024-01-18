using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClick : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = clip;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
