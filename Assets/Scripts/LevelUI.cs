using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] LevelData data;
    [SerializeField] Sprite[] planetSprites;
    [SerializeField] Image plantImage;

    private void Start()
    {
        plantImage.sprite = planetSprites[UnityEngine.Random.Range(0, planetSprites.Length)];
    }
}
