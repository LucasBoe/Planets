using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPostionHandle : InWorldHandle
{
    [SerializeField] RocketStartCurve limiter;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {
        transform.position = limiter.WorldPositionToCurve(settings.RocketPos);
    }

    public override void StartSimulation()
    {
        settings.RocketPos = transform.position;
        base.StartSimulation();
    }

    protected override Vector2 CalculateMousePosition()
    {
         return limiter.WorldPositionToCurve(base.CalculateMousePosition());
    }
}
