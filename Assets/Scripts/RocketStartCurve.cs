using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RocketStartCurve : SimulationBehaviour
{
    [SerializeField] Transform point1, point2, curve;
    [SerializeField] LineRenderer lineRendererTop, lineRendererBottom, lineRendererDirection;
    [SerializeField] SpriteRenderer dotTop, dotBottom;
    [SerializeField] int depth = 1;

    public Vector2[] Points;

    RocketPostionHandle rocketHandle;
    RocketRotationHandle rotationHandle;
    private void OnEnable()
    {
        Points = CalculateSmoothedPoints();
        rocketHandle = GetComponentInChildren<RocketPostionHandle>();
        rotationHandle = GetComponentInChildren<RocketRotationHandle>();
        rocketHandle.Init();
        rotationHandle.Init();
    }

    internal Vector2 GetMiddle()
    {
        return Points[Points.Length / 2];
    }

    public Vector2 GetCenter()
    {
        Vector3 pos = (point1.position + point2.position) / 2;
        return new Vector2(pos.x, pos.y);
    }

    public Vector2 GetDefaultLookAt()
    {
        Vector2 center = GetCenter();
        Vector2 negativeLook = ((Vector2)curve.position - center);
        return center - (negativeLook.normalized * 15);
    }

    private void OnDrawGizmos()
    {
        Vector2[] points = CalculateSmoothedPoints();

        Vector2 last = Vector2.zero;

        foreach (Vector2 point in points)
        {
            if (last != Vector2.zero)
                Gizmos.DrawLine(last, point);

            last = point;
        }

        Gizmos.DrawSphere(GetDefaultLookAt(), 2f);
    }

    private void Update()
    {
        if (rocketHandle != null)
        {
            UpdateCurveRenderers();

            if (rotationHandle != null)
                UpdateRotationLine();
        }
    }

    private void UpdateRotationLine()
    {

        Vector2 startPos = rocketHandle.transform.position;
        Vector2 targetPos = rotationHandle.transform.position;
        Vector2 endPos = startPos + (targetPos - startPos).normalized * 10;

        

        if (Vector2.Distance(startPos, endPos) > (rocketHandle.Radius + rotationHandle.Radius + 0.25f))
        {
            while (Vector2.Distance(startPos, rocketHandle.transform.position) < rocketHandle.Radius)
                startPos = Vector2.MoveTowards(startPos, endPos, 0.1f);

            while (Vector2.Distance(endPos, rotationHandle.transform.position) < rotationHandle.Radius)
                endPos = Vector2.MoveTowards(endPos, startPos, 0.1f);

            lineRendererDirection.SetPositions((new Vector2[] { startPos, endPos }).ToVector3Array(10));
        }
        else
        {
            lineRendererDirection.SetPositions((new Vector3[] { Vector3.zero, Vector3.zero }));
        }
    }

    private void UpdateCurveRenderers()
    {
        bool isBefore = true;

        List<Vector2> pBefore = new List<Vector2>();
        List<Vector2> pAfter = new List<Vector2>();

        for (int i = 0; i < Points.Length; i++)
        {
            Vector2 p = Points[i];

            if (Vector2.Distance(p, rocketHandle.transform.position) < rocketHandle.Radius)
            {
                if (isBefore)
                {
                    if (i > 0)
                    {
                        while (Vector2.Distance(p, rocketHandle.transform.position) < rocketHandle.Radius)
                        {
                            p = Vector2.MoveTowards(p, Points[i - 1], 0.1f);
                        }
                        pBefore.Add(p);
                    }

                    isBefore = false;
                }
                else
                {
                    if (i < (Points.Length - 1) && (Vector2.Distance(Points[i + 1], rocketHandle.transform.position) > rocketHandle.Radius))
                    {
                        while (Vector2.Distance(p, rocketHandle.transform.position) < rocketHandle.Radius)
                        {
                            p = Vector2.MoveTowards(p, Points[i + 1], 0.1f);
                        }
                        pAfter.Add(p);
                    }
                }
            }
            else
            {
                (isBefore ? pBefore : pAfter).Add(p);
            }
        }

        dotTop.enabled = pBefore.Count > 0;
        dotBottom.enabled = pAfter.Count > 0;

        lineRendererTop.positionCount = pBefore.Count;
        lineRendererTop.SetPositions(pBefore.ToArray().ToVector3Array(10));

        lineRendererBottom.positionCount = pAfter.Count;
        lineRendererBottom.SetPositions(pAfter.ToArray().ToVector3Array(10));
    }

    public override void StartSimulation()
    {
        Color moreTransparent = new Color(1, 1, 1, 0f);
        lineRendererTop.endColor = moreTransparent;
        lineRendererBottom.startColor = moreTransparent;

        lineRendererDirection.enabled = false;
    }

    internal Vector2 WorldPositionToCurve(Vector2 mousePos)
    {
        Vector2[] pointsByDistance = Points.OrderBy((d) => (d - mousePos).sqrMagnitude).ToArray();
        float distTo0 = Vector2.Distance(mousePos, pointsByDistance[0]);
        float distTo1 = Vector2.Distance(mousePos, pointsByDistance[1]);

        Debug.DrawLine(mousePos, pointsByDistance[0]);
        Debug.DrawLine(mousePos, pointsByDistance[1]);

        float distanceBetweenPoints = Vector2.Distance(pointsByDistance[0], pointsByDistance[1]);
        float dist = (GetBetaAngle(distanceBetweenPoints, distTo1, distTo0)) / distanceBetweenPoints;


        return Vector2.Lerp(pointsByDistance[0], pointsByDistance[1], dist);
    }

    float GetBetaAngle(float a, float b, float c)
    {
        return ((0.5f * Mathf.Pow(a, 2)) - (0.5f * Mathf.Pow(b, 2)) + (0.5f * Mathf.Pow(c, 2))) / (a);
    }

    private Vector2[] CalculateSmoothedPoints()
    {
        Vector2[] points = new Vector2[] { point1.position, curve.position, point2.position };



        for (int i = 0; i < depth; i++)
        {
            points = PointSmooth(points);
        }

        return points;
    }

    private Vector2[] PointSmooth(Vector2[] input)
    {
        List<Vector2> output = new List<Vector2>();

        output.Add(input[0]);

        for (int i = 0; i < input.Length - 1; i++)
        {
            float lerp = 0.8f - 0.6f * ((float)i / input.Length);
            output.Add(Vector2.Lerp(input[i], input[i + 1], lerp));
        }

        output.Add(input[input.Length - 1]);

        return output.ToArray();
    }
}
