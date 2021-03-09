using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnFootstep(Object audioClip)
    {
        audioSource.clip = (AudioClip)audioClip;
        audioSource?.Play();
    }
}
