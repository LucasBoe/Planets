using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPreviousPathDrawer : MonoBehaviour
{
    [SerializeField] RocketSettings settings;
    [SerializeField] LineRenderer linePrefab;
    [SerializeField] Color color;

    private void Start()
    {
        if (settings.previousPaths == null || settings.previousPaths.Count < 1)
            return;

        int pathCount = settings.previousPaths.Count;

        for (int i = 0; i < pathCount; i++)
        {
            float alpha = (float)(i + 1) / (float)pathCount;
            float width = Mathf.Lerp(0.05f, 0.1f, alpha);

            Path path = settings.previousPaths[i];
            LineRenderer line = Instantiate(linePrefab);
            line.positionCount = path.Points.Length;
            line.SetPositions(path.Points);
            line.widthCurve = AnimationCurve.Constant(0, 1, width);

            Color c = color;
            c.a = alpha;
            line.startColor = c;
            line.endColor = c;
        }
    }
}
