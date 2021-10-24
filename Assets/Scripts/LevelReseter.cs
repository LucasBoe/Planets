using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReseter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetLevel();
    }

    public void ResetLevel()
    {
        FindObjectOfType<LevelHandler>().ResetLevel();
    }
}
