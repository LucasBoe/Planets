using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRotationHandle : InWorldHandle
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Init()
    {
        if (PlayedBefore)
            transform.localPosition = settings.LookAtPos;
        else
            transform.position = startCurve.GetDefaultLookAt();
    }

    public override void StartSimulation()
    {
        settings.LookAtPos = transform.localPosition;
        base.StartSimulation();
    }
}
