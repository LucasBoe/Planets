using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHint : MonoBehaviour
{
    [SerializeField] InWorldHandle connectedHandle;

    // Start is called before the first frame update
    void Start()
    {
        connectedHandle.OnPointerDown += OnUse;
    }

    private void OnUse()
    {
        connectedHandle.OnPointerDown -= OnUse;
        Destroy(gameObject);
    }
}
