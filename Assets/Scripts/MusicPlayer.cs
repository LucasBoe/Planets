using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : SingletonBehaviour<MusicPlayer>
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
