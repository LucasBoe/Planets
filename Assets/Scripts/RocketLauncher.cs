using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public void Launch()
    {
        FindObjectOfType<Rocket>().Launch();
        Destroy(gameObject);
    }
}
