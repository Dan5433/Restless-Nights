using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class AudioSourceExtensions
    {
        public static void PlayWithRandomPitch(this AudioSource audioSource, AudioClip clip, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            audioSource.clip = clip;
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }
}
