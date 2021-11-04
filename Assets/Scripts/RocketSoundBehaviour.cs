using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSoundBehaviour : MonoBehaviour
{
    Rigidbody2D rocketBody;
    [SerializeField] AudioSource baseSpeedSource, launchSource, inOrbitSource, crashSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketBody = GetComponentInParent<Rigidbody2D>();
        Rocket rocket = GetComponentInParent<Rocket>();
        rocket.OnRocketLaunch += PlayLaunchSound;
        rocket.OnRocketCrash += PlayCrashSound;
    }

    private void PlayLaunchSound()
    {
        launchSource.Play();
    }

    private void PlayCrashSound()
    {
        transform.parent = null;
        baseSpeedSource.Stop();
        launchSource.Stop();
        inOrbitSource.Stop();
        crashSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketBody)
        {
            float speed = rocketBody.velocity.magnitude;
            baseSpeedSource.pitch = speed / 10f;
            inOrbitSource.volume = (speed - 10f) / 10f;
        } else
        {
            Destroy(gameObject);
        }
    }
}
