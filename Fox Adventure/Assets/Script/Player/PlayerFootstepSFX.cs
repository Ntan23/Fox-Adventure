using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSFX : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSource;

    void PlayFootstepSFX1()
    {
        audioSource[0].Play();
    }

    void PlayFootstepSFX2()
    {
        audioSource[1].Play();
    }
}
