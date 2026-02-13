using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerFlipAudio : MonoBehaviour
{
    [SerializeField] AudioSource circuitBreakerAudioSource;
    [SerializeField] AudioClip breakerOn;
    [SerializeField] AudioClip breakerOff;
    public void BreakerStateUpdate(float value)
    {
        if(value == 0)
        {
            circuitBreakerAudioSource.PlayOneShotWithRandomPitch(breakerOff);
        }
        else
        {
            circuitBreakerAudioSource.PlayOneShotWithRandomPitch(breakerOn);
        }
    }
}
