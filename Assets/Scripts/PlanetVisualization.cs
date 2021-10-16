using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetVisualization : MonoBehaviour
{
    PointEffector2D pointEffector2d;
    CircleCollider2D effectorCollider2d;

    // Start is called before the first frame update
    void Start()
    {
        float radius = 0;

        foreach (CircleCollider2D coll in GetComponents<CircleCollider2D>())
        {
            if (coll.usedByEffector)
                radius = coll.radius;
        }

        pointEffector2d = GetComponent<PointEffector2D>();

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        int pointCount = 36;
        int segmentLength = 360 / pointCount;
        List<Vector3> linePositions = new List<Vector3>();

        for (int i = 0; i <= 360; i += segmentLength)
        {
            Vector2 positionXY = new Vector2(Mathf.Sin(i * Mathf.Deg2Rad), Mathf.Cos(i * Mathf.Deg2Rad));
            Vector3 positionxyz = new Vector3(positionXY.x * radius, positionXY.y * radius, 0);
            linePositions.Add(positionxyz);
        }

        PlanetDataHolder planetDataHolderInstance = PlanetDataHolder.Instance;

        lineRenderer.loop = true;
        lineRenderer.positionCount = pointCount;
        lineRenderer.useWorldSpace = false;

        if (IsOrbiter())
        {
            lineRenderer.material = planetDataHolderInstance.OrbiterCircleMat;
            lineRenderer.textureMode = LineTextureMode.Tile;
        } else
        {
            lineRenderer.material = planetDataHolderInstance.PlanetOrbitCircleMat;
        }

        lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, 0.2f);
        lineRenderer.SetPositions(linePositions.ToArray());


        Color color = planetDataHolderInstance.Data[GetComponent<Planet>().Index].Color;
        SetLineRendererColor(lineRenderer, color, 0.34f);

        

        GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.white, 0.5f);
    }

    private bool IsOrbiter()
    {
        return (GetComponent<Planet>().IsOrbiter);
    }

    private static void SetLineRendererColor(LineRenderer lineRenderer, Color lineColor, float alpha)
    {
        lineColor.a = alpha;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }
}
