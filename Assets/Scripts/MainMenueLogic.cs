using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenueLogic : MonoBehaviour
{
    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
