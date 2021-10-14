using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContextLoader : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;

    private void Awake()
    {
        foreach (GameObject go in gameObjects)
        {
            Instantiate(go);
        }
    }
}
