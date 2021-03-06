using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public string Name;
    public int Astronauts, AstronautsMax;
    public bool Finished;

    public int SceneInBuildIndex;

    public bool ComingFromThatScene;

    public IntroData Intro;

    public bool Unlocked;
}
