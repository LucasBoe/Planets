using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPostionHandle : InWorldHandle
{
    [SerializeField] Rocket rocket;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Init()
    {
        transform.position = startCurve.WorldPositionToCurve(PlayedBefore ? settings.RocketPos : startCurve.GetCenter());
    }

    public override void StartSimulation()
    {
        settings.RocketPos = transform.position;
        rocket.transform.parent = null;
        base.StartSimulation();
        Destroy(gameObject);
    }

    protected override Vector2 CalculateMousePosition()
    {
        return startCurve.WorldPositionToCurve(base.CalculateMousePosition());
    }
}
