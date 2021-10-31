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
        List<Vector3> linePositions = Util.CreatePointsInCircle(radius, pointCount);

        PlanetDataHolder planetDataHolderInstance = PlanetDataHolder.Instance;

        lineRenderer.loop = true;
        lineRenderer.positionCount = pointCount;
        lineRenderer.useWorldSpace = false;

        if (IsOrbiter())
        {
            lineRenderer.material = planetDataHolderInstance.OrbiterCircleMat;
            lineRenderer.textureMode = LineTextureMode.Tile;
        }
        else
        {
            lineRenderer.material = planetDataHolderInstance.PlanetOrbitCircleMat;
        }

        lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, 0.2f);
        lineRenderer.SetPositions(linePositions.ToArray());


        Color color = planetDataHolderInstance.Data[GetComponent<Planet>().Index].Color;
        SetLineRendererColor(lineRenderer, color, 0.34f);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.material = new Material(spriteRenderer.material);
        spriteRenderer.material.SetInt("seed", name.GetHashCode());
        spriteRenderer.material.SetTexture("tex", planetDataHolderInstance.planetTextures[UnityEngine.Random.Range(0, planetDataHolderInstance.planetTextures.Length)]);
        spriteRenderer.material.SetVector("direction", new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
        spriteRenderer.material.SetColor("tint", new Color(color.r, color.g, color.b, 0));
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
