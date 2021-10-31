using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartUIBlender : MonoBehaviour
{
    [SerializeField] Transform[] uis;
    int indexVisible = 0;
    int IndexVisible
    {
        get => indexVisible;
        set
        {
            if (value != indexVisible)
            {
                indexVisible = value;
                UpdateVisibility(indexVisible);
            }
        }
    }

    private void UpdateVisibility(int indexVisible)
    {
        for (int i = 1; i < uis.Length; i++)
        {
            if (uis[i] != null)
            {
                uis[i].gameObject.SetActive(i == indexVisible);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < uis.Length; i++)
        {
            if (uis[i] != null && uis[i].localScale.magnitude > 0.01f)
            {
                IndexVisible = i;
                break;
            }
        }
    }
}
