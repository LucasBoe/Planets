using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public string Name;
    public int Astronauts, AtronautsMax;
    public bool Finished;
    public Scene Scene;
}
