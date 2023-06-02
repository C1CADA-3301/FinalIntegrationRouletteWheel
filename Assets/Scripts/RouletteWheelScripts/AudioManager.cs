using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    public Sound[] sounds;

    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

       foreach(Sound S in sounds)
        {
            
            S.source= gameObject.AddComponent<AudioSource>();
            S.source.clip = S.Clip;
            S.source.volume = S.Volume;
            S.source.pitch = S.Pitch;
            S.source.loop= S.Loop;
        }
    }

    private void Start()
    {
        //Play(AudioType.BgMusic);
    }

    public void Play(AudioType audiotype)
    {
        Debug.Log("audio play");

        Sound S =  Array.Find(sounds,Sound=>Sound.audioType==audiotype);
        if(S == null) 
        {
            Debug.LogWarning("Sound " + audiotype + " not found!");
            return; 
        }
        S.source.Play();
    }

    public void Stop(AudioType audiotype)
    {
        Sound S = Array.Find(sounds, Sound => Sound.audioType == audiotype);
        if (S == null)
        {
            Debug.LogWarning("Sound " + audiotype + " not found!");
            return;
        }
        S.source.Stop();
    }

    public void changeVolume(AudioType audioType, float volume)
    {
        Sound S = Array.Find(sounds, Sound => Sound.audioType == audioType);
        S.source.volume = volume;
    }

}
