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
    [SerializeField] LightSwitchTask lightSwitchTask;
    [SerializeField] Sprite lightSwitchOn;
    [SerializeField] Sprite lightSwitchOff;

    public Sprite LightSwitchOn => lightSwitchOn;
    public Sprite LightSwitchOff => lightSwitchOff;

    public void UpdateAllRoomLights()
    {
        if (circuitBreakerTask.MainBreaker.value == 0)
            return;

        foreach (RoomLightGroup lightGroup in lightGroups)
        {
            Slider breaker = lightGroup.roomBreaker;
            if (circuitBreakerTask.DisabledBreakers.Contains(breaker))
                continue;

            if (breaker.value == 1 && lightGroup.lightSwitch.IsOn)
                lightGroup.ChangeLightsState(true);
            else
                lightGroup.ChangeLightsState(false);
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

    public static void UpdateDoorwayLightAfterMovingRooms(Doorway origin, Doorway destination)
    {
        if (!IsInstanceValid())
            return;

        RoomLightGroup originGroup = Instance.lightGroups.First(g => g.doorways.Contains(origin));

        bool isOriginLightEnabled = originGroup.roomBreaker.value == 1 && originGroup.lightSwitch.IsOn;

        destination.DoorwayLight.gameObject.SetActive(isOriginLightEnabled);
    }
    public static void EnableAllRoomLights()
    {
        if (!IsInstanceValid())
            return;

        foreach (RoomLightGroup lightGroup in Instance.lightGroups)
            lightGroup.ChangeLightsState(true);
    }

    public static void LightSwitchStateUpdate()
    {
        if (!IsInstanceValid())
            return;

        Instance.UpdateAllRoomLights();

        if (Instance.lightSwitchTask.Active)
            Instance.lightSwitchTask.LightSwitchStateUpdate();
    }

    public static bool IsBreakerDisabled(LightSwitch lightSwitch)
    {
        if (!IsInstanceValid())
            return false;

        RoomLightGroup group = Instance.lightGroups.First(g => g.lightSwitch == lightSwitch);

        return group.roomBreaker.value == 0;
    }

    [Serializable]
    public struct RoomLightGroup
    {
        [Title(nameof(RoomName), stringInputMode: StringInputMode.Dynamic)]
        public Light2D roomLight;
        public Doorway[] doorways;
        public LightSwitch lightSwitch;
        public Slider roomBreaker;

        public readonly string RoomName => roomLight.transform.parent.gameObject.name;

        public readonly void ChangeLightsState(bool state)
        {
            roomLight.gameObject.SetActive(state);
            foreach (Doorway doorway in doorways)
                doorway.CurrentDestination
                    .DoorwayLight.gameObject.SetActive(state);
        }
    }
}
