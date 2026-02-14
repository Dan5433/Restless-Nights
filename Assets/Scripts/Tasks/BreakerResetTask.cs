using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakerResetTask : Task
{
    [SerializeField] Slider mainBreaker;
    [SerializeField] Slider[] roomBreakers;
    [SerializeField][DisableInEditMode, DisableInPlayMode] Slider[] disabledBreakers;
    [SerializeField] UIInteractable circuitBreakerInteractable;
    [SerializeField] AudioClip shakeCircuitBreakerBox;
    [SerializeField] BreakerFlipAudio breakerFlipAudio;

    const int MIN_BREAKERS = 1;

    public Slider MainBreaker => mainBreaker;
    public Slider[] DisabledBreakers => disabledBreakers;

    protected override IEnumerator TriggerTaskCoroutine()
    {
        circuitBreakerInteractable.PlaySFX(shakeCircuitBreakerBox);

        circuitBreakerInteractable.PlayInteractSFX();

        yield return new WaitWhile(() => circuitBreakerInteractable.IsAudioPlaying);

        yield return StartCoroutine(RandomizeBreakers());

        circuitBreakerInteractable.PlayCloseSFX();
    }

    protected override void Complete()
    {
        base.Complete();

        disabledBreakers = new Slider[0];

        LightManager.EnableAllRoomLights();
    }

    IEnumerator RandomizeBreakers()
    {
        WaitForSeconds waitBetweenActions = new(0.25f);

        yield return waitBetweenActions;

        List<int> breakers = new(roomBreakers.Length);
        for (int i = 0; i < roomBreakers.Length; i++)
            breakers.Add(i);

        int maxExtraBreakers = roomBreakers.Length - MIN_BREAKERS;

        int extraDisabledBreakers = Mathf.RoundToInt(TasksManager.Instance.DifficultyFraction * maxExtraBreakers);
        int disabledBreakersAmount = MIN_BREAKERS + extraDisabledBreakers;

        disabledBreakers = new Slider[disabledBreakersAmount];

        for (int i = 0; i < disabledBreakersAmount; i++)
        {
            int randomIndex = Random.Range(0, breakers.Count);

            int breakerIndex = breakers[randomIndex];

            Slider slider = roomBreakers[breakerIndex];
            slider.value = 0;
            disabledBreakers[i] = slider;

            breakers.Remove(breakerIndex);

            yield return new WaitWhile(() => breakerFlipAudio.IsPlaying);
        }

        yield return waitBetweenActions;
    }

    bool AllRoomBreakersEnabled()
    {
        foreach (Slider breaker in roomBreakers)
        {
            if (breaker.value == 0)
                return false;
        }

        return true;
    }

    public void MainBreakerStateUpdate(float sliderValue)
    {
        if (!active || sliderValue == 0 || !AllRoomBreakersEnabled())
            return;

        Complete();
    }
}
