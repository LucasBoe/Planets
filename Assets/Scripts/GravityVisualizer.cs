using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityVisualizer : SingletonBehaviour<GravityVisualizer>
{
    [SerializeField] Collider2D rocket;

    List<Planet> inGravityOf = new List<Planet>();
    float distanceBefore = 0;

    internal void EnterGravity(Planet planet)
    {
        if (!inGravityOf.Contains(planet))
            inGravityOf.Add(planet);
    }

    internal void LeaveGravity(Planet planet)
    {
        if (inGravityOf.Contains(planet))
            inGravityOf.Remove(planet);
    }

    private void FixedUpdate()
    {
        var emit = GetComponent<ParticleSystem>().emission;

        if (inGravityOf.Count <= 0)
        {
            emit.rateOverTime = Mathf.MoveTowards(emit.rateOverTime.constant, 0, 0.1f);
            distanceBefore = 0;
            return;
        }
        else
        {
            Vector3 rocketPos = rocket.transform.position;

            Planet current = null;
            float distance = float.MaxValue;

            foreach (Planet planet in inGravityOf)
            {
                float dist = Vector2.Distance(rocketPos, planet.transform.position);

                if (dist < distance)
                {
                    distance = dist;
                    current = planet;
                }
            }

            Vector3 planetPos = current.transform.position;

            float distanceNew = Vector2.Distance(rocketPos, planetPos);

            transform.position = Vector3.Lerp(rocketPos, planetPos, 0.3f);
            transform.LookAt(planetPos, Vector3.forward);

            float intensity = Mathf.Clamp(distanceBefore - distanceNew, 0, 100) * 20;

            emit.rateOverTime = Mathf.MoveTowards(emit.rateOverTime.constant, intensity, 0.1f);

            Debug.Log(intensity);

            distanceBefore = distanceNew;
        }
    }
}
