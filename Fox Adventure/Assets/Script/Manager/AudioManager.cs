using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance {get; private set;}
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }
    }
    #endregion
    [SerializeField] private Sound[] soundEffects;

    public void Play(string name)
    {
        Sound s = System.Array.Find(soundEffects,sound=>sound.name==name);

        if(s == null) return;

        s.source.PlayOneShot(s.clip);
    }
}