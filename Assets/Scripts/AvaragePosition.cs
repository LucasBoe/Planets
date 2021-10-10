using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaragePosition : MonoBehaviour
{
    [SerializeField] Transform[] toTakePositionFrom;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.zero;

        foreach (Transform t in toTakePositionFrom)
        {
            transform.position += t.position;
        }

        transform.position /= toTakePositionFrom.Length;
    }
}
