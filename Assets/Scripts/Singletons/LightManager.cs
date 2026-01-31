using EditorAttributes;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightManager : Singleton<LightManager>
{
    [SerializeField] RoomLightGroup[] lightGroups;
    [SerializeField] BreakerResetTask circuitBreakerTask;

    public void UpdateAllRoomLights()
    {
        if (circuitBreakerTask.MainBreaker.value == 0)
            return;

        foreach (RoomLightGroup lightGroup in lightGroups)
        {
            Slider breaker = lightGroup.roomBreaker;
            if (circuitBreakerTask.DisabledBreakers.Contains(breaker))
                continue;

            if (breaker.value == 0)
                lightGroup.ChangeLightsState(false);
            else
                lightGroup.ChangeLightsState(true);
        }
    }

    public void MainBreakerStateUpdate(float sliderValue)
    {
        if (sliderValue == 1)
            UpdateAllRoomLights();
        else
            MainBreakerOff();
    }

    void MainBreakerOff()
    {
        foreach (RoomLightGroup lightGroup in Instance.lightGroups)
            lightGroup.ChangeLightsState(false);
    }

    public static void EnableAllRoomLights()
    {
        if (!IsInstanceValid())
            return;

        foreach (RoomLightGroup lightGroup in Instance.lightGroups)
            lightGroup.ChangeLightsState(true);
    }

    [Serializable]
    public struct RoomLightGroup
    {
        [Title(nameof(RoomName), stringInputMode: StringInputMode.Dynamic)]
        public Light2D roomLight;
        public Light2D[] doorwayLights;
        public GameObject lightSwitch;
        public Slider roomBreaker;

        public string RoomName => roomLight.transform.parent.gameObject.name;

        public readonly void ChangeLightsState(bool state)
        {
            roomLight.gameObject.SetActive(state);
            foreach (Light2D light in doorwayLights)
                light.gameObject.SetActive(state);
        }
    }
}
