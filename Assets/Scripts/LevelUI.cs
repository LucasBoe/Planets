using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] LevelData data;
    [SerializeField] Texture2D[] planetSprites;

    [SerializeField] CanvasGroup fokusVisuals;
    [SerializeField] Image planetImage;
    [SerializeField] Transform astronautDisplay;
    [SerializeField] Image checkmark;
    [SerializeField] TMP_Text nameText, astronautText;

    LevelSelection levelSelection;

    internal void Setup(LevelData data)
    {
        Color c = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), 1, 1);
        c.a = 0;
        planetImage.material = new Material(planetImage.material);
        planetImage.material.SetColor("tint", c);
        planetImage.material.SetTexture("tex", PlanetDataHolder.Instance.planetTextures[UnityEngine.Random.Range(0, PlanetDataHolder.Instance.planetTextures.Length)]);
        this.data = data;
        nameText.text = data.Name;
        checkmark.enabled = data.Finished;
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
