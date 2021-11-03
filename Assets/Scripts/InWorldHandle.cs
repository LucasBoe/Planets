using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldHandle : SimulationBehaviour
{
    public bool AutoRadius;
    public float BaseRadius;
    public float Radius => visuals.localScale.x * BaseRadius;
    public bool PlayedBefore => settings.previousPaths != null && settings.previousPaths.Count > 0;

    [SerializeField] Transform visuals;
    [SerializeField] protected RocketStartCurve startCurve;
    [SerializeField] protected RocketSettings settings;
    [SerializeField] protected Vector3 offset;

    protected Camera main;

    public Action OnPointerDown;
    public Action OnPointerUp;
    public Action OnPointerEnter;
    public Action OnPointerExit;

    public virtual void Init()
    {

    }

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
    private void OnMouseDown() { OnPointerDown?.Invoke(); }
    private void OnMouseUp() { OnPointerUp?.Invoke(); }
    private void OnMouseEnter() { OnPointerEnter?.Invoke(); }
    private void OnMouseExit() { OnPointerExit?.Invoke(); }
}
