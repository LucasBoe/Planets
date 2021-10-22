using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static List<Vector3> CreatePointsInCircle(float radius, int pointCount)
    {
        int segmentLength = 360 / pointCount;
        List<Vector3> linePositions = new List<Vector3>();

        for (int i = 0; i <= 360; i += segmentLength)
        {
            Vector2 positionXY = new Vector2(Mathf.Sin(i * Mathf.Deg2Rad), Mathf.Cos(i * Mathf.Deg2Rad));
            Vector3 positionxyz = new Vector3(positionXY.x * radius, positionXY.y * radius, 0);
            linePositions.Add(positionxyz);
        }

        return linePositions;
    }
}
