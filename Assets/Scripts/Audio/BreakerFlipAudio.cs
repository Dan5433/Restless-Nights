using Extensions;
using UnityEngine;

public class BreakerFlipAudio : MonoBehaviour
{
    [SerializeField] AudioSource circuitBreakerAudioSource;
    [SerializeField] AudioClip breakerOn;
    [SerializeField] AudioClip breakerOff;

    public bool IsPlaying => circuitBreakerAudioSource.isPlaying;

    public void BreakerStateUpdate(float value)
    {
        if (value == 0)
        {
            circuitBreakerAudioSource.PlayOneShotWithRandomPitch(breakerOff);
        }
        else
        {
            circuitBreakerAudioSource.PlayOneShotWithRandomPitch(breakerOn);
        }
    }
}
