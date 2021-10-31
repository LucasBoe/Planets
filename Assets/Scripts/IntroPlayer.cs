using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroPlayer : SingletonBehaviour<IntroPlayer>
{
    public bool Hidden = true;

    AudioSource noiseSource;
    TMP_Text textDisplay;
    Button skipButton;
    private bool skip;

    private void Start()
    {
        skipButton = GetComponentInChildren<Button>();
        skipButton.onClick.AddListener(OnSkip);
        skipButton.gameObject.SetActive(false);
        textDisplay = GetComponentInChildren<TMP_Text>();
        noiseSource = GetComponent<AudioSource>();
        transform.localScale = Vector3.zero;
    }

    private void OnSkip()
    {
        skip = true;
    }

    internal void PlayIntro(IntroData intro)
    {
        StartCoroutine(IntroRoutine(intro));
    }

    private IEnumerator IntroRoutine(IntroData introData)
    {
        string[] texts = introData.Text;
        float animationDuration = 0.3f;

        if (Hidden)
        {
            SoundHandler.Play(BaseSounds.RadioOnOff);
            float scaleAnimationFactor = 0;
            while (scaleAnimationFactor < animationDuration)
            {
                Debug.LogWarning(scaleAnimationFactor);
                transform.localScale = Vector3.one * Mathf.Pow((scaleAnimationFactor / animationDuration), 3);
                scaleAnimationFactor += Time.deltaTime;
                yield return null;
            }
            Hidden = false;
        }

        yield return new WaitForSeconds(1f);

        int textIndex = 0;
        while (textIndex < texts.Length)
        {
            SoundHandler.Play(BaseSounds.RadioOnOff);
            noiseSource.Play();

            string text = texts[textIndex];
            int charactersMax = text.Length;
            for (int charactersVisible = 0; charactersVisible < charactersMax; charactersVisible++)
            {
                string newText = text.Substring(0, charactersVisible + 1) + "<alpha=#00>" + text.Substring(charactersVisible + 1, text.Length - charactersVisible - 1);
                textDisplay.text = newText;
                yield return new WaitForSeconds(GetPauseByCharacter(text[charactersVisible]));
            }

            noiseSource.Pause();
            SoundHandler.Play(BaseSounds.RadioOnOff);
            skipButton.gameObject.SetActive(true);

            while (skip == false)
                yield return null;

            skipButton.gameObject.SetActive(false);
            skip = false;
            textIndex++;
        }

        if (!Hidden)
        {
            SoundHandler.Play(BaseSounds.RadioOnOff);
            float scaleAnimationFactor = animationDuration;
            while (scaleAnimationFactor > 0f)
            {
                transform.localScale = Vector3.one * Mathf.Pow(scaleAnimationFactor / animationDuration, 3);
                scaleAnimationFactor -= Time.deltaTime;
                yield return null;
            }
            Hidden = true;
        }
    }

    private float GetPauseByCharacter(char c)
    {
        switch (c)
        {
            case '.':
                return 0.5f;
            case ',':
                return 0.5f;
            case ' ':
                return 0.1f;
            default:
                return 0.05f;
        }
    }
}

[System.Serializable]
public class IntroData
{
    public string[] Text;
}