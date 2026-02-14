using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioClip lightSwitchOn;
    [SerializeField] AudioClip lightSwitchOff;
    [SerializeField] AudioClip lightSwitchFail;

    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closeDoor;

    public AudioClip LightSwitchOn => lightSwitchOn;
    public AudioClip LightSwitchOff => lightSwitchOff;
    public AudioClip LightSwitchFail => lightSwitchFail;

    public AudioClip OpenDoor => openDoor;
    public AudioClip CloseDoor => closeDoor;
}
