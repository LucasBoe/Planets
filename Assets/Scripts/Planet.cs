using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : SimulationBehaviour
{
    public int Index;
    [SerializeField] float rotationSpeed;

    [SerializeField] GameObject glowPrefab;

    [SerializeField] Planet toOrbit;

    protected Rigidbody2D rigidbody2D;
    protected PointEffector2D pointEffector2D;

    float distance = 0;
    public bool IsOrbiter => toOrbit != null;

    void OnEnable()
    {
        Instantiate(glowPrefab, transform).transform.localScale = new Vector3(10, 10, 10);
        rigidbody2D = GetComponent<Rigidbody2D>();
        pointEffector2D = GetComponent<PointEffector2D>();

        Debug.Log(pointEffector2D == null);

        pointEffector2D.enabled = false;
    }

    public override void StartSimulation()
    {
        if (IsOrbiter)
            InitiateOrbit();



        Debug.Log(name + (pointEffector2D == null).ToString());

        pointEffector2D.enabled = true;
    }

    private void InitiateOrbit()
    {
        toOrbit.RegisterOrbiter(this);

        distance = Vector2.Distance(transform.position, toOrbit.transform.position);
        float force = (1f / Mathf.Sqrt(distance)) * Mathf.PI;
        Vector2 direction = Vector2.Perpendicular((toOrbit.transform.position - transform.position).normalized);
        rigidbody2D.velocity = (direction * force);
    }

    private void RegisterOrbiter(Planet planet)
    {
        LayerMask layerMaskToAdd = PlanetDataHolder.Instance.Data[planet.Index].Layer;
        LayerMask layerMaskBefore = pointEffector2D.colliderMask;
        pointEffector2D.colliderMask = layerMaskBefore.AddLayerMask(layerMaskToAdd); ;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rocket>())
            GravityVisualizer.Instance.EnterGravity(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rocket>())
            GravityVisualizer.Instance.LeaveGravity(this);
    }
    private void OnDrawGizmosSelected()
    {
        if (!IsOrbiter)
            return;

        distance = Vector2.Distance(transform.position, toOrbit.transform.position);
        Gizmos.DrawWireSphere(toOrbit.transform.position, distance);
    }
}
