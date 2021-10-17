using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RocketStartCurve : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform point1, point2, curve;
    [SerializeField] int depth= 1;

    public Vector2[] Points;

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Points = CalculateSmoothedPoints();
        lineRenderer.positionCount = Points.Length;
        lineRenderer.SetPositions(Points.ToVector3Array(10));
    }

    internal Vector2 GetMiddle()
    {
        return Points[Points.Length / 2];
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
    }

    internal Vector2 WorldPositionToCurve(Vector2 mousePos)
    {
        Vector2[] pointsByDistance = Points.OrderBy((d) => (d - mousePos).sqrMagnitude).ToArray();
        float distTo0 = Vector2.Distance(mousePos, pointsByDistance[0]);
        float distTo1 = Vector2.Distance(mousePos, pointsByDistance[1]);

        Debug.DrawLine(mousePos, pointsByDistance[0]);
        Debug.DrawLine(mousePos, pointsByDistance[1]);

        float distanceBetweenPoints = Vector2.Distance(pointsByDistance[0] , pointsByDistance[1]);
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
            output.Add(Vector2.Lerp(input[i], input[i+1], lerp));
        }

        output.Add(input[input.Length - 1]);

        return output.ToArray();
    }
}
