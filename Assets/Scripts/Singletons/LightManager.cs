using EditorAttributes;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightManager : Singleton<LightManager>
{
    [SerializeField, DataTable] RoomLightGroup[] lightGroups;
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
                lightGroup.roomLight.gameObject.SetActive(false);
            else
                lightGroup.roomLight.gameObject.SetActive(true);
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
            lightGroup.roomLight.gameObject.SetActive(false);
    }

    public static void EnableAllRoomLights()
    {
        if (!IsInstanceValid())
            return;

        foreach (RoomLightGroup lightGroup in Instance.lightGroups)
            lightGroup.roomLight.gameObject.SetActive(true);
    }

    [Serializable]
    public struct RoomLightGroup
    {
        public Light2D roomLight;
        public GameObject lightSwitch;
        public Slider roomBreaker;
    }
}
