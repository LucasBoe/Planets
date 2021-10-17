using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotationHandle : InWorldHandle
{
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localPosition = settings.LookAtPos;
    }

    public override void StartSimulation()
    {
        settings.LookAtPos = transform.localPosition;
        base.StartSimulation();
    }
}
