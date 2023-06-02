
using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{

    // public string Name;
    public AudioType audioType;
    public AudioClip Clip;

    [Range(0,2)]
    public float Volume;
    [Range(0, 3)]
    public float Pitch;

    public bool Loop;
    [HideInInspector]
    public AudioSource source;
    
}




