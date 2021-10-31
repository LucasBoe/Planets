using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundHandler : SingletonBehaviour<SoundHandler>
{
    [SerializeField] AudioMixer soundMixer;
    [SerializeField] AudioClip uiClick, radioOnOff;

    public static void Play(BaseSounds sound)
    {
        Instance.StartCoroutine(Instance.PlaySoundRoutine(Instance.EnumToClip(sound)));
    }

    private AudioClip EnumToClip(BaseSounds sound)
    {
        switch (sound)
        {
            case BaseSounds.RadioOnOff:
                return radioOnOff;
        }
        return uiClick;
    }

    private IEnumerator PlaySoundRoutine(AudioClip sound)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.volume = 0.5f;
        audio.Play();
        audio.outputAudioMixerGroup = soundMixer.FindMatchingGroups("Master")[0];
        yield return new WaitForSeconds(1f);
        Destroy(audio);
    }
}

public enum BaseSounds
{
    UIClick,
    RadioOnOff
}
