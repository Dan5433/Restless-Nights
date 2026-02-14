using EditorAttributes;
using Extensions;
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
    [SerializeField] AudioSource breakerAudioSource;

    const int MIN_BREAKERS = 1;

    public Slider MainBreaker => mainBreaker;
    public Slider[] DisabledBreakers => disabledBreakers;

    protected override void TriggerInternal()
    {
        StartCoroutine(TriggerTaskCoroutine());
    }

    IEnumerator TriggerTaskCoroutine()
    {
        AudioSource audioSource = circuitBreakerInteractable.AudioSource;

        audioSource.PlayOneShotWithRandomPitch(shakeCircuitBreakerBox);

        AudioClip openCircuitBox = circuitBreakerInteractable.InteractSFX;
        audioSource.PlayOneShotWithRandomPitch(openCircuitBox);

        yield return new WaitWhile(() => audioSource.isPlaying);

        yield return StartCoroutine(RandomizeBreakers());

        AudioClip closeCircuitBox = circuitBreakerInteractable.CloseUiSFX;
        audioSource.PlayOneShotWithRandomPitch(closeCircuitBox);
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

            yield return new WaitWhile(() => breakerAudioSource.isPlaying);

            disabledBreakers[i] = slider;

            breakers.Remove(breakerIndex);
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
