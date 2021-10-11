using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] LevelData data;
    [SerializeField] Sprite[] planetSprites;

    [SerializeField] CanvasGroup fokusVisuals;
    [SerializeField] Image plantImage;
    [SerializeField] Transform astronautDisplay;
    [SerializeField] TMP_Text nameText, astronautText;

    LevelSelection levelSelection;

    internal void Setup(LevelData data)
    {
        plantImage.sprite = planetSprites[UnityEngine.Random.Range(0, planetSprites.Length)];
        this.data = data;
        nameText.text = data.Name;
        astronautDisplay.gameObject.SetActive(data.AstronautsMax > 0);
        astronautText.text = data.Astronauts + " / " + data.AstronautsMax;

        levelSelection = GetComponentInParent<LevelSelection>();
    }

    public void Select()
    {
        levelSelection.Fokus(this, data);
    }

    public void Play()
    {
        levelSelection.Play(data);
    }

    internal void SetFokused(bool inFokus)
    {
        fokusVisuals.gameObject.SetActive(inFokus);
    }
}
