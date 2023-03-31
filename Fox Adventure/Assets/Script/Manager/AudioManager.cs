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

    private void Play(string name)
    {
        Sound s = System.Array.Find(soundEffects,sound=>sound.name==name);

        if(s == null) return;

        s.source.PlayOneShot(s.clip);
    }

    public void PlayFootstepSFX()
    {
        int randomIndex = Random.Range(0,2);

        if(randomIndex == 0) Play("Footstep1");
        else Play("Footstep2");
    }

    public void PlayHitSFX()
    {
        Play("Hit");
    }

    public void PlayJumpSFX()
    {
        Play("Jump");
    }

    public void PlayFallSFX()
    {
        Play("Fall");
    }

    public void PlayCollectCollectablesSFX()
    {
        Play("CollectCollectables");
    }

    public void PlayHurtSFX()
    {
        Play("Hurt");
    }

    public void PlayFallToPitSFX()
    {
        Play("FallToPit");
    }

    public void PlayUIPopUpSFX()
    {
        Play("UIPopUp");
    }

    public void PlayVictorySFX()
    {
        Play("Victory");
    }

    public void PlayLoseSFX()
    {
        Play("Lose");
    }

    public void PlayHoverSFX()
    {
        Play("Hover");
    }

    public void PlayClickSFX()
    {
        Play("Click");
    }
}