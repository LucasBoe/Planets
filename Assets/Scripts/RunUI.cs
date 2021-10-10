using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunUI : MonoBehaviour
{
    Rocket rocket;

    [SerializeField] TMP_Text counterTextUI;
    private void Start()
    {
        rocket = FindObjectOfType<Rocket>();
    }

    private void Update()
    {
        counterTextUI.text = rocket.Astronauts.ToString();
    }
}
