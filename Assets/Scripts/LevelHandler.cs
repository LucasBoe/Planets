using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : SingletonBehaviour<LevelHandler>
{
    [SerializeField] GameObject transitionPrefab;

    public LevelData LevelData;
    public int Astronauts;

    private void Start()
    {
        var lds = Resources.LoadAll("", typeof(LevelData));

        foreach (LevelData data in lds)
        {
            if (SceneManager.GetActiveScene().buildIndex == data.SceneInBuildIndex)
                LevelData = data;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ReachedTarget()
    {
        Debug.Log("ReachedTarget");

        if (LevelData.Astronauts < Astronauts)
            LevelData.Astronauts = Astronauts;

        LevelData.Finished = true;
        Instantiate(transitionPrefab);

        Invoke("ReturnToLevelSelection",2);
    }

    private void ReturnToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
