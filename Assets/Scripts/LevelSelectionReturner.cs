using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionReturner : MonoBehaviour
{
    public void ReturnToLevelSelectionMenue()
    {
        LevelHandler.Instance.ReturnToLevelSelection();
    }
}
