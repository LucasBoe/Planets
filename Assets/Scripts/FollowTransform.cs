using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform ToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = ToFollow.position;
    }
}
