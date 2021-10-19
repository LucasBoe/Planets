using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunUI : MonoBehaviour
{
    LevelHandler levelHandler;

    [SerializeField] TMP_Text counterTextUI;
    private void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();
    }

    private void Update()
    {
        counterTextUI.text = levelHandler.Astronauts.ToString();
    }
}
