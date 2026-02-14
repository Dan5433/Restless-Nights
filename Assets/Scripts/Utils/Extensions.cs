using UnityEngine;

namespace Extensions
{
    public static class AudioSourceExtensions
    {
        public static void PlayOneShotWithRandomPitch(this AudioSource audioSource, AudioClip clip, float minPitch = 0.95f, float maxPitch = 1.05f)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(clip);
        }
    }
}
