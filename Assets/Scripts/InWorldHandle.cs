using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldHandle : SimulationBehaviour
{
    public float Radius = 1f;
    [SerializeField] protected RocketSettings settings;
    [SerializeField] protected Vector3 offset;


    protected Camera main;

    protected virtual void OnEnable()
    {
        main = Camera.main;
    }

    public override void StartSimulation()
    {
        Destroy(this);
    }

    private void OnMouseDrag()
    {
        Vector2 MousePos = CalculateMousePosition();

        transform.position = (Vector3)MousePos + offset;
    }

    protected virtual Vector2 CalculateMousePosition()
    {
        return (Vector2)main.ScreenToWorldPoint(Input.mousePosition);
    }
}
