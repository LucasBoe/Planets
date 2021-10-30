using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : SingletonBehaviour<SoundHandler>
{
    [SerializeField] AudioClip uiClick;

    public static void Play(BaseSounds sound)
    {
        Instance.StartCoroutine(Instance.PlaySoundRoutine(Instance.EnumToClip(sound)));
    }

    private AudioClip EnumToClip(BaseSounds sound)
    {
        return uiClick;
    }

    private IEnumerator PlaySoundRoutine(AudioClip sound)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.volume = 0.5f;
        audio.Play();
        yield return new WaitForSeconds(1f);
        Destroy(audio);
    }
}

public enum BaseSounds
{
    UIClick
}
