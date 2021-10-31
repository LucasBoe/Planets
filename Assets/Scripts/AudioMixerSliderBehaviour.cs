using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioMixerSliderBehaviour : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    Slider slider;
    const string volume = "Volume";

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        float value;
        audioMixer.GetFloat(volume, out value);
        slider.value = value;

        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        audioMixer.SetFloat(volume, value);
    }
}
